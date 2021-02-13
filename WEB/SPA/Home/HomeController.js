app.controller("HomeController", function ($scope, $cookieStore, $window, blockUI) {
    setTimeout(function () {
        $('.mycounter').counterUp({
            delay: 10,
            time: 1000
        });
    }, 100);
});