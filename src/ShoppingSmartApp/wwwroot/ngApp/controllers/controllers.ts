namespace ShoppingSmartApp.Controllers {

    export class ShoppingListController {
        // General controller for the maintance of the Shopping List of the consumers
        private ShoppingListResource;
        private SuperMarketListResource;

        public shoppinglist;
        public shoppinglists;
        public supermarketsList;
        public validationErrors;

        constructor(private $resource: angular.resource.IResourceService, 
            private $state: ng.ui.IStateService, private accountService: ShoppingSmartApp.Services.AccountService,
            private $http: ng.IHttpService) 
        {
            this.ShoppingListResource = $resource('/api/shoppinglists/:id' , null, {
                findDeal: {
                    method: 'GET',
                    url: '/api/shoppinglists/findDeal/:id/:zipcode',
                    isArray: false
                },
                getMyLists: {
                    method: 'GET',
                    url: '/api/shoppinglists/getMyLists/:username',
                    isArray: true
                } 
            });

            this.shoppinglist = { Username: this.accountService.getUserName() };
            this.SuperMarketListResource = $resource('/api/supermarkets/:id');
            this.getShoppingList();
            this.getSuperMarkets();
        }

        public goAddShoppingList() {

            this.$state.go('addshoppinglist');

        }

        public goShoppingList() {

            this.$state.go('shoppinglist', { reload: true });

        }

        public getSuperMarkets() {

            this.supermarketsList = this.SuperMarketListResource.query();
        }

        public getShoppingList() {

            this.shoppinglists = this.ShoppingListResource.getMyLists({ username: this.accountService.getUserName() });
        }

        public cancelForm() {
            this.shoppinglist = null;
            this.validationErrors = null;
            this.$state.go('shoppinglist');
        }

        public save() {

            this.ShoppingListResource.save(this.shoppinglist).$promise.then(() => {

                this.goShoppingList();

            }).catch((err) => {

                let validationError = [];
                for (let prop in err.data) {
                    let propError = err.data[prop];
                    validationError = validationError.concat(propError);
                }
                this.validationErrors = validationError;
                });
        }

        public checkDeals(id: any) {
           
            this.$state.go('findbestdeal', { id: id });
        }

        public goListDetail(id: number) {
       
            this.$state.go('listdetail', { shoppinglistid: id });
        }
    }

    export class ProductListController {

        // Controller for List of Universal Products (non necesary in List) review

        private ProductListResource;
        private ProductResource;
        private ShoppingListResource;

        public product;
        public products;
        public shoppingproduct;
        public shoppingproducts;
        public validationErrors;
        public shoppinglist;

        public getProducts() {
            this.products = this.ProductResource.query();
        }

        public getShoppingProducts(id: number) {

            this.shoppingproducts = this.ProductListResource.getProductsByShoppingList({ id: id });
        }

        public getShoppingList(id: number) {
            this.shoppinglist = this.ShoppingListResource.get({ id: id });
        }

        public goAddProductToList(id: number) {

            this.$state.go('productlist', { shoppinglistid: id });

        }

        public goListDetail(id: number) {

            this.$state.go('listdetail', { shoppinglistid: id });
        }

        public addToShoppingList(shoppinglistid: number, shoppinglistname: string, productid: number, productname: string) {

            //Perform the Open Modal function for to Add Universal Products to the List selected.

            var modalEnvironment = this.$uibModal.open({
                templateUrl: '/ngApp/views/Shopping/modalshoppingproduct.html',
                controller: 'ShoppingProductListController', //See definition and details ahead (below)
                controllerAs: 'modal',
                resolve: {
                    shoppinglistid: () => shoppinglistid,
                    shoppinglistname: () => shoppinglistname,
                    productid: () => productid,
                    productname: () => productname
                },
                size: 'md'
            })
        }

        public deleteShoppingProduct(id: number) {

            this.ProductListResource.remove({ id: id }).$promise.then(() => {
                this.getShoppingProducts(this.shoppinglist.id);
            });
          
        }

        constructor(private $resource: angular.resource.IResourceService,
            private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService,
            private $uibModal: angular.ui.bootstrap.IModalService) {

            this.ShoppingListResource = $resource('/api/shoppinglists/:id');
            this.ProductResource = $resource('/api/products/:id');
            this.ProductListResource = $resource('/api/shoppingproducts/:id', null, {
                getProductsByShoppingList: {
                    method: 'GET',
                    url: '/api/shoppingproducts/getProductsByShoppingList/:id',
                    isArray: true
                }
            });
         
            let shoppinglistid = $stateParams['shoppinglistid'];
            this.getShoppingList(shoppinglistid);
            this.getShoppingProducts(shoppinglistid);                        
            this.getProducts();
        }
    }

    class ShoppingProductListController {

        // Controller of modal view for to add a product into a Shopping List selected.
        private ShoppingProductResource;

        public shoppingproduct;
        public shoppingproducts;
        public validationErrors;

        /* public getShoppingProducts() {
 
             this.shoppingproducts = this.ShoppingProductResource.query();
         }*/

        public init($scope) {
            $scope.modal.shoppingproduct = { shoppinglistid: this.shoppinglistid, productid: this.productid };
        }


        constructor(private $resource: angular.resource.IResourceService,
            private $http: ng.IHttpService, private $scope: angular.IScope,
            private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance,
            private shoppinglistid: any, private shoppinglistname: any,
            private productid: any, private productname: any) {
            this.shoppingproduct = {};
            this.init($scope);
            this.ShoppingProductResource = $resource('/api/shoppingproducts/:id');
        }

        public save() {

            this.ShoppingProductResource.save(this.shoppingproduct).$promise.then(() => {
                this.shoppingproduct = null;
                this.validationErrors = null;
                this.closeDialog();
            }).catch((err) => {

                let validationError = [];
                for (let prop in err.data) {
                    let properr = err.data[prop];
                    validationError = validationError.concat(properr);
                }
                this.validationErrors = validationError;
            })
        }

        public closeDialog() {
            this.$uibModalInstance.close();
        }

    }

    angular.module('ShoppingSmartApp').controller('ShoppingProductListController', ShoppingProductListController);


    export class FindBestDealController {

        private FindBestDealResource;

        public bestDeal;
        public totalSupermarkets; 
        public listProducts = [];
        public zipcodeToSearch = "";
        public listData;
        public supermarketSelected = null;
        public isGo; //For to show results section in the view (init no to show)
        public noResult; //Flag for result of Evaluate if there at least one Supermarket deal for the list

        constructor(private $resource: angular.resource.IResourceService,
            private $stateParams: ng.ui.IStateParamsService,
            private $http: ng.IHttpService, private $uibModal: angular.ui.bootstrap.IModalService,
            public $scope: angular.IScope) {

            this.isGo = false; 
            this.noResult = false; 
            this.FindBestDealResource = $resource('/api/shoppinglists/:id', null, {
                findDeal: {
                    method: 'GET',
                    url: '/api/shoppinglists/findDeal/:id/:zipcode',
                    isArray: true
                }
            });

            var id = $stateParams['id'];
            this.listData = this.FindBestDealResource.get({id: id});
        }

        // To find the supermarkets that match the list and location (zipcode)
        public finddeal(id: number, zipcode: string) {
            this.isGo = false; 
            this.noResult = false; 
            this.FindBestDealResource.findDeal({ id: id, zipcode: zipcode }).$promise.then((data) => {
                    
                this.totalSupermarkets = data;
                console.log(this.totalSupermarkets);
                this.isGo = true; 
                if (data.length == 0) {

                    this.noResult = true;
                }
            }); 
        }

        //To show the products of the deal (supermarket) selected from the List of deals
        public showProducts(supermarketselectedid: number) {

            var modalEnvironment = this.$uibModal.open({
                templateUrl: '/ngApp/views/Shopping/modalproducts.html',
                controller: 'ProductsSelectedController', //See definition and details ahead (below)
                controllerAs: 'modal',
                resolve: {
                    supermarketData: () => this.totalSupermarkets.filter((s) =>
                                                    s.superMarketId == supermarketselectedid)
                    },
                    size: 'lg'
                });
        }
    }

    angular.module('ShoppingSmartApp').controller('FindBestDealController', FindBestDealController);

    class ProductsSelectedController {
        // Controller for the modal that show the products of the deal (supermarket) selected from the List of deals

        private SupermarketResource;

        public supermarket;
        public bestDeal;
        public totalSupermarkets = {};
        public listProducts = [];
        public isSent;
        public listData;

        constructor(private $resource: angular.resource.IResourceService,
            private $http: ng.IHttpService, private $uibModal: angular.ui.bootstrap.IModalService,
            public supermarketData: any, 
            private $uibModalInstance: angular.ui.bootstrap.IModalServiceInstance) {
       
            this.totalSupermarkets = supermarketData[0];
            this.SupermarketResource = $resource('/api/supermarkets/:id');
            this.isSent = false;
            this.getSupermarket(supermarketData[0].superMarketId);
        }

        public getSupermarket(id: number) {

            this.supermarket = this.SupermarketResource.get({ id: id });
        }

        //For this version is just a flag, future version will start the integration with Supermarkets Delivery/Pickup Systems/Module
        public sendToSupermarket() {
            this.isSent = true;
        }
        public closeDialog() {
            this.$uibModalInstance.close();
        }
    }

    angular.module('ShoppingSmartApp').controller('ProductsSelectedController', ProductsSelectedController);
   
    export class HomeController {

    }
    

    export class AboutController {
        public message = 'Copyright © 2016, $mart$hopping App, All protective rights reserved.  ';
    }
}