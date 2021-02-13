app.controller("LoginController", function ($scope, $cookieStore, $http, $window, blockUI) {
    $window.localStorage.clear();
    $scope.user = {};
    $scope.loginUser = {};
    $scope.loginFailAlert = false;
    $scope.loginFailMessage = '';
    $scope.IsMaintenance = 0;
    //SetIsAuthentic();
    //GetMaintenanceInfo();
    alertify.set({ buttonReverse: true });

    function SetIsAuthentic() {
        $http({
            url: "/Login/SetIsAuthentic?isAuthentic=0",
            method: 'POST',
            headers: { 'Content-Type': 'application/json' }
        })
    }

    function GetMaintenanceInfo() {
        $http({
            url: '/Login/GetMaintenanceInfo',
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            var infoArr = data.split('~');
            $scope.IsMaintenance = infoArr[0];
            if ($scope.IsMaintenance == 1) {
                $scope.loginFailMessage = infoArr[1];
                $scope.loginFailAlert = true;
                $scope.MaintenancePin = infoArr[2];
            }
        });
    }

    function doLogin() {
        $scope.loginFailAlert = false;

        blockUI.start();
        $http({
            url: "/Login/GetWebUserForLogin?email=" + $scope.user.Username + "&passcode=" + $scope.user.Password,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            if (data.length) {
                var objOne = data[0];
                if (data.length === 1 && objOne.SQLMessage !== null && objOne.SQLMessage !== '') {
                    $scope.loginFailMessage = objOne.SQLMessage;
                    blockUI.stop();
                    $scope.loginFailAlert = true;

                } else {
                    var lst = JSON.stringify(data);
                    $window.localStorage.setItem('permissionList', lst);

                    $cookieStore.remove('UserData');
                    $cookieStore.put('UserData', $scope.user);

                    window.location = '/Home/Index#/Home';
                }
            } else {
                $scope.loginFailMessage = 'System could not retrive user information';
                $scope.loginFailAlert = true;
                blockUI.stop();
            }
        }).error(function (data4) {
            $scope.loginFailMessage = 'Server Error, please refresh page';
            $scope.loginFailAlert = true;
            blockUI.stop();
        });
    }

    $scope.dismissAlert = function () {
        $scope.loginFailAlert = false;
    }

    $scope.Login = function () {
        if ($scope.IsMaintenance == 1) {
            setTimeout(function () {
                alertify.prompt('Enter Maintenance Passcode', function (e, val) {
                    if (e) {
                        if (val === $scope.MaintenancePin)
                            doLogin();
                        else
                            alertify.log('Incorrect Maintenance Passcode!', 'error', '3000');
                    }
                })
            }, 200);
        }
        else {
            doLogin();
        }
    };  
});
