namespace ShoppingSmartApp {

    angular.module('ShoppingSmartApp', ['ui.router', 'ngResource', 'ui.bootstrap', 'ngAnimate']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider, 
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: ShoppingSmartApp.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: ShoppingSmartApp.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: ShoppingSmartApp.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: ShoppingSmartApp.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: ShoppingSmartApp.Controllers.AboutController,
                controllerAs: 'controller'
            })
            .state('supermarket', {
                url: '/supermarket',
                templateUrl: '/ngApp/views/SuperMarket/listsupermarket.html',
                controller: ShoppingSmartApp.Controllers.SuperMarketController,
                controllerAs: 'controller'
            })
            .state('addsupermarket', {
                url: '/supermarket/addsupermarket',
                templateUrl: '/ngApp/views/SuperMarket/addsupermarket.html',
                controller: ShoppingSmartApp.Controllers.SuperMarketController,
                controllerAs: 'controller'
            })
            .state('editsupermarket', {
                url: '/supermarket/editsupermarket/:id',
                templateUrl: '/ngApp/views/SuperMarket/editsupermarket.html',
                controller: ShoppingSmartApp.Controllers.EditSuperMarketController,
                controllerAs: 'controller'
            })
            .state('deletesupermarket', {
                url: '/supermarket/deletesupermarket/:id',
                templateUrl: '/ngApp/views/SuperMarket/deletesupermarket.html',
                controller: ShoppingSmartApp.Controllers.DeleteSuperMarketController,
                controllerAs: 'controller'
            })
            .state('product', {
                url: '/product',
                templateUrl: '/ngApp/views/Product/index.html',
                controller: ShoppingSmartApp.Controllers.ProductController,
                controllerAs: 'controller'
            })
            .state('addproduct', {
                url: '/product/addproduct',
                templateUrl: '/ngApp/views/Product/addproduct.html',
                controller: ShoppingSmartApp.Controllers.ProductController,
                controllerAs: 'controller'
            })
            .state('editproduct', {
                url: '/product/editproduct/:id',
                templateUrl: '/ngApp/views/Product/editproduct.html',
                controller: ShoppingSmartApp.Controllers.EditProductController,
                controllerAs: 'controller'
            })
            .state('deleteproduct', {
                url: '/product/deleteproduct/:id',
                templateUrl: '/ngApp/views/Product/deleteproduct.html',
                controller: ShoppingSmartApp.Controllers.DeleteProductController,
                controllerAs: 'controller'
            })
            .state('catalog', {
                url: '/catalog/:supermarketid',
                templateUrl: '/ngApp/views/SuperMarket/catalog.html',
                controller: ShoppingSmartApp.Controllers.ProductCatalogController,
                controllerAs: 'controller'
            })
            .state('addcatalog', {
                url: '/catalog/addcatalog/:supermarketid',
                templateUrl: '/ngApp/views/SuperMarket/addcatalog.html',
                controller: ShoppingSmartApp.Controllers.ProductCatalogController,
                controllerAs: 'controller'
            })
            .state('shoppinglist', {
                url: '/shoppinglist',
                templateUrl: '/ngApp/views/Shopping/shoppinglist.html',
                controller: ShoppingSmartApp.Controllers.ShoppingListController,
                controllerAs: 'controller'
            })
            .state('addshoppinglist', {
                url: '/shoppinglist/addshoppinglist',
                templateUrl: '/ngApp/views/Shopping/addshoppinglist.html',
                controller: ShoppingSmartApp.Controllers.ShoppingListController,
                controllerAs: 'controller'
            })
            .state('listdetail', {
                url: '/shoppinglist/listdetail/:shoppinglistid',
                templateUrl: '/ngApp/views/Shopping/listdetail.html',
                controller: ShoppingSmartApp.Controllers.ProductListController,
                controllerAs: 'controller'
            })
            .state('productlist', {
                url: '/shoppinglist/productlist/:shoppinglistid',
                templateUrl: '/ngApp/views/Shopping/productlist.html',
                controller: ShoppingSmartApp.Controllers.ProductListController,
                controllerAs: 'controller'
            })
            .state('findbestdeal', {
                url: '/shoppinglist/findbestdeal/:id',
                templateUrl: '/ngApp/views/Shopping/findbestdeal.html',
                controller: ShoppingSmartApp.Controllers.FindBestDealController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    
    angular.module('ShoppingSmartApp').factory('authInterceptor', (
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

    angular.module('ShoppingSmartApp').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

}
