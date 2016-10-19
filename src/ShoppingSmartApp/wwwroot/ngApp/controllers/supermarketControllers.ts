namespace ShoppingSmartApp.Controllers {

    export class SuperMarketController {

        private SuperMarketResource;

        public supermarket;
        public supermarkets;
        public validationErrors;
        public searchText;
        public states;

        public getSuperMarkets() {
            this.supermarkets = this.SuperMarketResource.query();
        }

        public save() {
            this.SuperMarketResource.save(this.supermarket).$promise.then(() => {
                this.cancelForm();
            }).catch((err) => {
                let validationErrors = [];
                for (let prop in err.data) {
                    let propErrors = err.data[prop];
                    validationErrors = validationErrors.concat(propErrors);
                }
                this.validationErrors = validationErrors;
            });
        }

        public goAddSuperMarket() {
            this.$state.go('addsupermarket');
        }

        public goEditSuperMarket(id: number) {
            this.$state.go('editsupermarket', { id: id });
        }

        public goDeleteSuperMarket(id: number) {
            this.$state.go('deletesupermarket', { id: id });
        }

        public goCatalog(id: number) {
            this.$state.go('catalog', { supermarketid: id });
        }

        public cancelForm() {
            this.supermarket = null;
            this.validationErrors = null;
            this.$state.go('supermarket');
        }

        public getStates() {

            this.states = this.addressServices.getStates();
        }

        constructor(private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService,
            private addressServices: ShoppingSmartApp.Services.AddressServices) {
            this.SuperMarketResource = $resource('/api/supermarkets/:id');
            this.getSuperMarkets();
            this.getStates();
        }
    }

    export class EditSuperMarketController {

        private SuperMarketResource;

        public supermarket;
        public validationErrors;
        public states;

        public save() {
            this.SuperMarketResource.save(this.supermarket).$promise.then(() => {
                this.cancelForm();
            }).catch((err) => {
                let validationErrors = [];
                for (let prop in err.data) {
                    let propErrors = err.data[prop];
                    validationErrors = validationErrors.concat(propErrors);
                }
                this.validationErrors = validationErrors;
            });
        }

        public cancelForm() {
            this.supermarket = null;
            this.validationErrors = null;
            this.$state.go('supermarket');
        }

        public getStates() {

        this.states = this.addressServices.getStates();
        }

        constructor(private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService,
            private addressServices: ShoppingSmartApp.Services.AddressServices) {
            this.SuperMarketResource = $resource('/api/supermarkets/:id');
            let id = $stateParams['id'];
            this.supermarket = this.SuperMarketResource.get({ id: id });
            this.getStates();
        }
    }

    export class DeleteSuperMarketController {

        private SuperMarketResource;

        public supermarket;
        public validationErrors;
        public states;

        public delete(id: number) {
            this.SuperMarketResource.remove({id: id}).$promise.then(() => {
                this.cancelForm();
            }).catch((err) => {
                let validationErrors = [];
                for (let prop in err.data) {
                    let propErrors = err.data[prop];
                    validationErrors = validationErrors.concat(propErrors);
                }
                this.validationErrors = validationErrors;
            });
        }

        public cancelForm() {
            this.supermarket = null;
            this.validationErrors = null;
            this.$state.go('supermarket');
        }

        public getStates() {
            this.states = this.addressServices.getStates();
        }

        constructor(private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService,
            private addressServices: ShoppingSmartApp.Services.AddressServices) {
            this.SuperMarketResource = $resource('/api/supermarkets/:id');
            let id = $stateParams['id'];
            this.supermarket = this.SuperMarketResource.get({ id: id });
            this.getStates();
        }
    }

    export class ProductCatalogController {

        private ProductCatalogResource;
        private ProductResource;
        private SuperMarketResource;

        public product;
        public products;
        public productcatalog;
        public productcatalogs;
        public validationErrors;
        public supermarket;

        public getProductCatalogs(supermarketid: number) {

            this.productcatalogs = this.ProductCatalogResource.getAllbySupermarket({ id: supermarketid });

        }

        public getSupermarket(id: number) {
            this.supermarket = this.SuperMarketResource.get({ id: id });
        }

        public getProducts() {
            this.products = this.ProductResource.query();
        }

        public deleteProductCatalog(id: number) {
            this.ProductCatalogResource.remove({ id: id }).$promise.then(() => {
                this.getProductCatalogs(this.supermarket.id);
            });
        }
        
        public goAddCatalog(id: number) {
            debugger;
               this.$state.go('addcatalog', { supermarketid: id });
        }

        public goCatalog(id: number) {
            this.$state.go('catalog', { supermarketid: id });
        }

        public addToCatalog(supermarketid: number, supermarketname: string, productid: number, productname: string) {

            //Perform the Open Modal function

            var modalEnvironment = this.$uibModal.open({
                templateUrl: '/ngApp/views/SuperMarket/modalcatalog.html',
                controller: 'CatalogController', //See definition and details ahead (below)
                controllerAs: 'modal',
                resolve: {
                    supermarketid: () => supermarketid,
                    supermarketname: () => supermarketname,
                    productid: () => productid,
                    productname: () => productname
                },
                size: 'md'
            })

        }

        constructor(private $resource: angular.resource.IResourceService,
            private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService,
            private $uibModal: angular.ui.bootstrap.IModalService) {

            this.SuperMarketResource = $resource('/api/supermarkets/:id');
            this.ProductResource = $resource('/api/products/:id');
            this.ProductCatalogResource = $resource('/api/productcatalogs/:id', null, {
                getAllbySupermarket: {
                    method: 'GET',
                    url: '/api/productcatalogs/getAllbySupermarket/:id',
                    isArray: true
                }
            });

            let supermarketid = $stateParams['supermarketid'];
            this.getSupermarket(supermarketid);
            this.getProductCatalogs(supermarketid);
            this.getProducts();
        }
    }

    class CatalogController {

        private ProductCatalogResource;

        public productcatalog;
        public validationErrors

        public getProductCatalog(id: number) {

            this.productcatalog = this.ProductCatalogResource.get({ id: id });
        }

        public save() {

            this.ProductCatalogResource.save(this.productcatalog).$promise.then(() => {
                this.productcatalog = null;
                this.validationErrors = null;
                this.$uibModalInstance.close();
            }).catch((err) => {
                let validationErrors = [];
                for (let prop in err.data) {
                    let propErrors = err.data[prop];
                    validationErrors = validationErrors.concat(propErrors);
                }
                this.validationErrors = validationErrors;
            });
        }

        public init($scope) {
            $scope.modal.productcatalog = { supermarketid: this.supermarketid, productid: this.productid };
        }
        public closeDialog() {
            this.$uibModalInstance.close();
        }

        constructor(private $scope: angular.IScope, private $resource: angular.resource.IResourceService,
            private $uibModal: angular.ui.bootstrap.IModalService,
            private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance, private $http: ng.IHttpService,
            private supermarketid: number, private supermarketname: string, private productid: number, private productname: string) {

            this.productcatalog = {};
            this.init($scope);
            this.ProductCatalogResource = $resource('/api/productcatalogs/:id');

        }
    }

    angular.module('ShoppingSmartApp').controller('CatalogController', CatalogController);
}