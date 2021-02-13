app.controller("PermissionCtrl", function ($scope, $cookieStore, $http) {
    $scope.Screenlist = [];
    $scope.DetailList = [];
    $scope.ConfirmationMessageForAdmin = false;

    //For Screen lock
    $scope.LoginUser = $cookieStore.get('UserData');
    $scope.UserId = $scope.LoginUser.UserId;
    $scope.ScreenId = $cookieStore.get('PermissionScreenId');
    $scope.ScreenLockInfo = [];

    //Lock Screen by user
    function ScreenLock() {
        $http({
            url: '/Permission/CheckScreenLock',
            method: 'GET',
            params: { userId: $scope.UserId, screenId: $scope.ScreenId },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data != '') {
                $scope.ScreenLockInfo = data;
                alertify.alert('This page is locked by ' + $scope.ScreenLockInfo[0].Username);
                window.location = '/Home/Index#/Home';
            }
            else {
                $scope.s_ScreenLock = new Object();
                $scope.s_ScreenLock.UserId = $scope.UserId;
                $scope.s_ScreenLock.ScreenId = $scope.ScreenId;
                var parms = JSON.stringify({ screenLock: $scope.s_ScreenLock });
                $http.post('/Permission/CreateScreenLock', parms).success(function (data) {
                });
            }
        });
    }

    function GetConfirmationMessageForAdmin() {
        $http({
            url: '/Role/GetConfirmationMessageForAdmin',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.ConfirmationMessageForAdmin = (data === 'true');
        });
    }

    function Clear(isFromSave) {
        $scope.s_Role = new Object();
        $scope.ddlRole = new Object();
        $scope.selectAllCheckBox = new Object();
        CheckUncheckAll(false, isFromSave);
    }

    function GetAllRole() {
        $http({
            url: '/UserGroup/Get',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Rolelist = data;
        });
    }

    function GetAllScreen() {
        $http({
            url: '/Permission/GetAllScreen',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.Screenlist = data;
            //     GetScreenDetail();
        });
    }

    function GetScreenDetail() {
        angular.forEach($scope.Screenlist, function (aScreen) {
            $http({
                url: '/Permission/GetDetailByScreenId?screenId=' + aScreen.ScreenId,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                aScreen.DetailList = data;
            });
        })
    }

    function CheckUncheckAll(flag, isFromSave) {
        angular.forEach($scope.Screenlist, function (aScreen) {
            aScreen.selected = flag;
            angular.forEach(aScreen.DetailList, function (detail) {
                detail.CanExecute = flag;
                if (flag) {
                    var gotInList = false;
                    angular.forEach($scope.DetailList, function (scpDetail) {
                        if (scpDetail.ScreenId == aScreen.ScreenId && scpDetail.ScreenDetailId == detail.ScreenDetailId) {
                            gotInList = true;
                        }
                    })
                    if (!gotInList) {
                        $scope.DetailList.push({ ScreenId: aScreen.ScreenId, ScreenDetailId: detail.ScreenDetailId, CanExecute: true });
                    }
                }
            })
        });

        if (!flag && !isFromSave) {
            $scope.DetailList = [];
        }
    };

    function GetPermissionByRoleId(roleId) {

        var asd = "moga";

        $http({
            url: '/Login/GetByUserGroupId?userGroupId=' + roleId,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            $scope.permissionList = Enumerable.From(data).Select("x => { PermissionId: x['PermissionId'], ScreenId: x['ScreenId'], CanView: x['CanView'], ScreenName: x['ScreenName'], ModuleName: x['ModuleName'] }").ToArray();

            $scope.Screenlist = Enumerable.From($scope.permissionList).Distinct(function (x) { return x.ScreenId; }).Select(function (a) { return a; }).ToArray();

            angular.forEach($scope.Screenlist, function (aScreen) {
                aScreen.selected = aScreen.CanView
                aScreen.DetailList = Enumerable.From(data).Where('$.ScreenId === ' + aScreen.ScreenId).ToArray();
            })

            //angular.forEach($scope.Screenlist, function (aScreen) {
            //    angular.forEach(data, function (aPermission) {
            //        if (aScreen.ScreenId == aPermission.ScreenId) {
            //            aScreen.selected = aPermission.CanView;

            //            //if (aPermission.CanView) {
            //            //    angular.forEach(aScreen.DetailList, function (aScreenDetail) {
            //            //        angular.forEach(data.DetailList, function (aPermissionDtl) {
            //            //            if (aScreenDetail.ScreenDetailId == aPermissionDtl.ScreenDetailId) {
            //            //                aScreenDetail.CanExecute = aPermissionDtl.CanExecute;
            //            //            }
            //            //        })
            //            //    })
            //            //    $scope.DetailList = $scope.DetailList.concat(data.DetailList);

            //            //}


            //            if (aPermission.CanView) {
            //                $http({
            //                    url: '/Permission/GetDetailByPermissionId?permissionId=' + aPermission.PermissionId,
            //                    method: 'GET',
            //                    headers: { 'Content-Type': 'application/json' }
            //                }).success(function (permissionDtlLst) {
            //                    angular.forEach(aScreen.DetailList, function (aScreenDetail) {
            //                        angular.forEach(permissionDtlLst, function (aPermissionDtl) {
            //                            if (aScreenDetail.ScreenDetailId == aPermissionDtl.ScreenDetailId) {
            //                                aScreenDetail.CanExecute = aPermissionDtl.CanExecute;
            //                            }
            //                        })
            //                    })
            //                    $scope.DetailList = $scope.DetailList.concat(permissionDtlLst);
            //                })
            //            }
            //        }
            //    });
            //});
        });
    };

    function AddPermissionList(roleId) {
        roleId = roleId.Id;
        var PermissionLst = [];
        var DetailList = [];
        angular.forEach($scope.Screenlist, function (aScreen) {
            $scope.s_Permission = new Object();
            $scope.s_Permission.RoleId = roleId;
            $scope.s_Permission.ScreenId = aScreen.ScreenId;
            $scope.s_Permission.CanView = aScreen.selected;
            $scope.s_Permission.CreatorId = "1"//$scope.UserId;
            $scope.s_Permission.UpdatorId = "1";//$scope.UserId;
            PermissionLst.push($scope.s_Permission);
            angular.forEach(aScreen.DetailList, function (screenDetail) {
                DetailList.push(screenDetail);
                //if (screenDetail.ScreenId == aScreen.ScreenId) {
                //    $scope.s_PermissionDetail = new Object();
                //    $scope.s_PermissionDetail.ScreenId = aScreen.ScreenId;
                //    $scope.s_PermissionDetail.PermissionId = 0;
                //    $scope.s_PermissionDetail.ScreenDetailId = screenDetail.ScreenDetailId;
                //    $scope.s_PermissionDetail.CanExecute = screenDetail.CanExecute;
                //    DetailList.push($scope.s_PermissionDetail);
                //}
            })
        });
        $.ajax({
            url: "/Permission/SavePermissionWithDetails",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            type: "POST",
            data: JSON.stringify({ roleId: roleId, PermissionLst: PermissionLst, DetailList: DetailList }),
            success: function (data) {
                if (data > 0) {
                    alertify.log('Permission Saved Successfully', 'success', '100000');
                    Clear(true);
                } else { alertify.log('Server Save Errors!', 'error', '10000'); }
            }, error: function (msg) {
                alertify.log('Server Save Errors!', 'error', '10000');
            }
        });
    };

 //   ScreenLock();

  //  GetConfirmationMessageForAdmin();

    Clear(false);

    GetAllRole();

    GetAllScreen();

    //Events
    $scope.SelectRole = function (item) {
        CheckUncheckAll(false, false);
        $scope.selectAllCheckBox = false;
        GetPermissionByRoleId(item.Id);
    };

    $scope.AddPermission = function (roleId) {
        if ($scope.ConfirmationMessageForAdmin) {
            alertify.confirm("Are you sure to save?", function (e) {
                if (e) {
                    AddPermissionList(roleId);
                }
            });
        }
        else {
            AddPermissionList(roleId);
        }
    }

    $scope.SelectAllCheckBox = function (selectAllCheckBox) {
        CheckUncheckAll(selectAllCheckBox);
    };

    $scope.SelectScreen = function (aScreen) {
        angular.forEach(aScreen.DetailList, function (detail) {
            detail.CanExecute = aScreen.selected;

            if (aScreen.selected) {
                var gotInList = false;
                angular.forEach($scope.DetailList, function (scpDetail) {
                    if (scpDetail.ScreenId == aScreen.ScreenId && scpDetail.ScreenDetailId == detail.ScreenDetailId) {
                        gotInList = true;
                    }
                })
                if (!gotInList) {
                    $scope.DetailList.push({ ScreenId: aScreen.ScreenId, ScreenDetailId: detail.ScreenDetailId, CanExecute: true });
                }
            }
            else {
                var index = 0;
                angular.forEach($scope.DetailList, function (scpDetail) {
                    if (scpDetail.ScreenId == aScreen.ScreenId && scpDetail.ScreenDetailId == detail.ScreenDetailId) {
                        index = $scope.DetailList.indexOf(scpDetail);
                    }
                })
                $scope.DetailList.splice(index, 1);
            }
        });
    };

    $scope.ChkDetail = function (aScreen, screenDetailId, isChecked) {
        if (isChecked) {
            $scope.DetailList.push({ ScreenId: aScreen.ScreenId, ScreenDetailId: screenDetailId, CanExecute: true });
        }
        else {
            var index = 0;
            angular.forEach($scope.DetailList, function (scpDetail) {
                if (scpDetail.ScreenId == aScreen.ScreenId && scpDetail.ScreenDetailId == screenDetailId) {
                    index = $scope.DetailList.indexOf(scpDetail);
                }
            })
            $scope.DetailList.splice(index, 1);
        }

        var loop = 0;
        var allDetailUnChecked = 0;

        angular.forEach(aScreen.DetailList, function (detail) {
            if (!detail.CanExecute) {
                allDetailUnChecked += 1;
            }
            loop += 1;
        });

        if (allDetailUnChecked == loop) {
            aScreen.selected = aScreen.ScreenName == 'Dashboard Summary' ? true : false;
        }
        else {
            aScreen.selected = true;
        }
    };

    $scope.ResetForm = function () {
        Clear(false);
        $scope.permissionEntryForm.$setPristine();
        $scope.permissionEntryForm.$setUntouched();
    };
});