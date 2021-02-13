var app = angular.module('csmApp', ['ngRoute', 'ngCookies', 'ngAnimate', 'ngMaterial', 'jkAngularRatingStars', 'blockUI', 'angularUtils.directives.dirPagination']);

app.config(function ($routeProvider, blockUIConfig) {
    $routeProvider
        .when('/Home', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
		})
		.when('/User', {
			templateUrl: '/SPA/User/User.html',
			controller: 'UserCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/UserGroup', {
			templateUrl: '/SPA/UserGroup/UserGroup.html',
			controller: 'UserGroupCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/Permission', {
			templateUrl: '/SPA/Permission/Permission.html',
			controller: 'PermissionCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
        })
        .when('/ChangePassword', {
            templateUrl: '/SPA/ChangePassword/ChangePassword.html',
            controller: 'ChangePasswordCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/SchemeNumber', {
            templateUrl: '/SPA/SchemeNumber/SchemeNumber.html',
            controller: 'SchemeNumberCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
		.when('/OutletType', {
			templateUrl: '/SPA/OutletType/OutletType.html',
			controller: 'OutletTypeCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
		.when('/CommentType', {
			templateUrl: '/SPA/CommentType/CommentType.html',
			controller: 'CommentTypeCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
        }).when('/AIC', {
            templateUrl: '/SPA/AIC/AIC.html',
            controller: 'AICCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        }).when('/ASM', {
            templateUrl: '/SPA/ASM/ASM.html',
            controller: 'ASMCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .when('/Distributorlist', {
            templateUrl: '/SPA/Distributorlist/Distributorlist.html',
            controller: 'DistributorlistCtrl',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
		.when('/Comment', {
			templateUrl: '/SPA/Comment/Comment.html',
			controller: 'CommentCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
        .when('/SurveyReports', {
			templateUrl: '/SPA/Reports/Reports.html',
			controller: 'ReportsCtrl',
			//resolve: {
			//    "check": function ($cookieStore) {
			//        var login = $cookieStore.get('UserData');
			//        if (angular.isUndefined(login) || login == null) {
			//            //window.location = '/Home/Login';
			//        }
			//    }
			//}
		})
        .when('/', {
            templateUrl: '/SPA/Home/Home.html',
            controller: 'HomeController',
            //resolve: {
            //    "check": function ($cookieStore) {
            //        var login = $cookieStore.get('UserData');
            //        if (angular.isUndefined(login) || login == null) {
            //            //window.location = '/Home/Login';
            //        }
            //    }
            //}
        })
        .otherwise({ redirectTo: '/' });

    blockUIConfig.template = '<div class="block-ui-overlay"></div><div class="block-ui-message-container"> <img src="../img/loading.gif" /> <h4><strong>LOADING...</strong></h4> </div>'
});