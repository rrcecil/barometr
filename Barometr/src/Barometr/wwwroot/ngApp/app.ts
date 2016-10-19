namespace Barometr {

    angular.module('Barometr', ['ui.router', 'ngResource', 'ui.bootstrap', 'google.places', 'ngGPlaces', 'angular-input-stars']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider,
        ngGPlacesAPIProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: Barometr.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('bar', {
                url: '/bar/:id',
                templateUrl: '/ngApp/views/bar.html',
                controller: Barometr.Controllers.BarController,
                controllerAs: 'controller'
            })
            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: Barometr.Controllers.SecretController,
                controllerAs: 'controller'
            })
            .state('profile', {
                url: '/profile',
                templateUrl: '/ngApp/views/profile.html',
                controller: Barometr.Controllers.ProfileController,
                controllerAs: 'controller'
            })
            .state('myreviews', {
                url: '/myreviews',
                templateUrl: '/ngApp/views/myreviews.html',
                controller: Barometr.Controllers.ProfileReviewsController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: Barometr.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: Barometr.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: Barometr.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: Barometr.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            })
           .state('search', {
               url: '/search',
               templateUrl: '/ngApp/views/search.html',
             controller: Barometr.Controllers.SearchController,
               controllerAs: 'controller'
           })
            .state('barProfile', {
                url: '/barProfile',
                templateUrl: '/ngApp/views/barProfile.html',
                controller: Barometr.Controllers.BarController,
                controllerAs: 'controller'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);

        ngGPlacesAPIProvider.setDefaults({
            radius: 7000,
            type: 'bar'
        });
    });

    
    angular.module('Barometr').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('Barometr').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

    

}
