app.controller("TopBarController", function ($scope, $cookieStore, $http, $window) {
    $scope.loggedinUser = $cookieStore.get('UserData');

    $scope.logOut = function () {
        window.location = '/Home/Login';
    }
});