app.controller("ChangePasswordCtrl", function ($scope, $cookieStore, $window, $location, $http, blockUI) {
    $scope.DefaultPerPage = 20;
    $scope.currentPage = 1;
    $scope.PerPage = $scope.DefaultPerPage;
    $scope.total_count = 0;
    $scope.entityList = [];
    $scope.entityListPaged = [];
    $scope.entryBlock = blockUI.instances.get('entryBlock');
    $scope.lsitBlock = blockUI.instances.get('lsitBlock');
    clear();
    getList();
    getUserGroup();
    function clear() {
        $scope.entity = { Id: 0, IsActive: true };
        $("#txtFocus").focus();
        $scope.OldPassword = "";
        $scope.NewPassword = "";
        $scope.RetypenewPassword = "";
    };
    function getUserGroup() {


        $http({
            url: "/UserGroup/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                $scope.userGroupList = data;
            }
        });
    }
    function getList() {
        $scope.lsitBlock.start();
        $http({
            url: "/User/Get",
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {

            if (data.length) {
                $scope.lsitBlock.stop();

                $scope.entityList = data;
                $scope.total_count = data.length;

                var begin = ($scope.PerPage * ($scope.currentPage - 1));
                var end = begin + $scope.PerPage;
                $scope.entityListPaged = $scope.entityList.slice(begin, end);
            }
            else {
                $scope.lsitBlock.stop();
                //alertify.log('System could not retrive information, please refresh page', 'error', '10000');
            }

        }).error(function (data2) {
            $scope.lsitBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    function submitRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity, transactionType: trnType });

        $http.post('/User/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    $scope.resetForm();
                    getList();
                    alertify.log(data, 'success', '5000');
                }
                else {
                    $scope.entryBlock.stop();
                    alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                }
            }
            else {
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            $scope.entryBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };
    function submitPasswordRequest(trnType) {
        var params = JSON.stringify({ obj: $scope.entity[0], transactionType: "UPDATE" });

        $http.post('/User/Post', params).success(function (data) {
            $scope.entryBlock.start();
            if (data != '') {
                if (data.indexOf('successfully') > -1) {
                    $scope.entryBlock.stop();
                    $scope.resetForm();                    
                    alertify.log(data, 'success', '5000');
                    $scope.loggedinUser = $cookieStore.get('UserData');
                    $scope.loggedinUser.Password = $scope.Password_C;
                    $cookieStore.put('UserData', $scope.loggedinUser);
                }
                else {
                    $scope.entryBlock.stop();
                    alertify.log('System could not execute the operation. ' + data, 'error', '10000');
                }
            }
            else {
                $scope.entryBlock.stop();
                alertify.log('System could not execute the operation.', 'error', '10000');
            }
        }).error(function () {
            $scope.entryBlock.stop();
            alertify.log('Unknown server error', 'error', '10000');
        });
    };

    $scope.GetPaged = function (curPage) {
        $scope.currentPage = curPage;
        $scope.PerPage = (angular.isUndefined($scope.PerPage) || $scope.PerPage == null) ? $scope.DefaultPerPage : $scope.PerPage;

        if ($scope.PerPage > 100) {
            $scope.PerPage = 100;
            alertify.log('Maximum record  per page is 100', 'error', '5000');
        }
        else if ($scope.PerPage < 1) {
            $scope.PerPage = 1;
            alertify.log('Minimum record  per page is 1', 'error', '5000');
        }

        var begin = ($scope.PerPage * (curPage - 1));
        var end = begin + $scope.PerPage;

        $scope.entityListPaged = $scope.entityList.slice(begin, end);
    }

    $scope.post = function (trnType) {
        var where = "CommentsType = '" + $scope.entity.CommentsType + "'";
        if ($scope.entity.Id > 0)
            where += " AND Id <> " + $scope.entity.Id;

        $http({
            url: '/User/GetDynamic?where=' + where + '&orderBy=CommentsType',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length > 0) {
                alertify.log($scope.entity.CommentsType + ' already exists!', 'already', '5000');
                $('#txtFocus').focus();
            } else {
                if (trnType === 'save') {
                    trnType = $scope.entity.Id === 0 ? "INSERT" : "UPDATE";
                    submitRequest(trnType);
                }

                else {
                    trnType = "DELETE";

                    alertify.set({
                        labels: {
                            ok: "Yes",
                            cancel: "No"
                        },
                        buttonReverse: true
                    });

                    alertify.confirm('Are you sure to delete?', function (e) {
                        if (e) {
                            submitRequest(trnType);
                        }
                    });
                }
            }
        });
    };

    $scope.postPassword = function (trnType) {
        $scope.loggedinUser = $cookieStore.get('UserData');
        var where = "Email = '" + $scope.loggedinUser.Username + "'";
        var _password = $scope.loggedinUser.Password
        if (_password == $scope.OldPassword) {
            if ($scope.NewPassword == $scope.RetypenewPassword) {
                $http({
                    url: '/User/GetDynamic?where=' + where + '&orderBy=Id',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    if (data.length > 0) {
                        
                        if (trnType === 'save') {
                            $scope.entity = data;
                            $scope.entity[0].Password = $scope.RetypenewPassword;
                            $scope.Password_C = $scope.RetypenewPassword;
                            submitPasswordRequest("UPDATE");
                        }


                    }
                });
            }
            else {
                alertify.log('New Password Does Not Match!', 'already', '5000');
            }
        }
        else {
            alertify.log('Old Password Does Not Match!', 'already', '5000');
        }
        
    };
    $scope.rowClick = function (obj) {
        $scope.entity = obj;
        $('#txtFocus').focus();
    };

    $scope.resetForm = function () {
        clear();
        $scope.frm.$setUntouched();
        $scope.frm.$setPristine();
    };
})