namespace ShoppingSmartApp.Controllers {
    export class ProductController {

        private ProductResource;

        public product;
        public products;
        public validationErrors;
        public units;
        
        public getProducts() {
            this.products = this.ProductResource.query();
            console.log('products ' + this.products);
        }

        public save() {
            this.ProductResource.save(this.product).$promise.then(() => {
                this.cancelForm();
                this.getProducts();
            }).catch((err) => {
                let validationError = [];
                for (let prop in err.data) {
                    let properror = err.data[prop];
                    validationError = validationError.concat(properror);
                }
                this.validationErrors = validationError;
            });

        }

        public goAddProduct() {
            this.$state.go('addproduct');
        }

        public goEditProduct(id: number) {
            this.$state.go('editproduct', { id: id });
        }

        public goDeleteProduct(id: number) {
            this.$state.go('deleteproduct', { id: id });
        }
        public cancelForm() {
            this.product = null;
            this.validationErrors = null;
            this.$state.go('product');
        }

        constructor(private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,private productServices: ShoppingSmartApp.Services.ProductServices) {

            this.ProductResource = $resource('/api/products/:id');
            this.getProducts();
            this.units = this.productServices.getUnits();
        }
    }

    export class EditProductController {

        private ProductResource;

        public product;
        public units;
        public validationErrors;

        public save() {
            this.ProductResource.save(this.product).$promise.then(() => {
                this.cancelForm();
            }).catch((err) => {
                let validationError = [];
                for (let prop in err.data) {
                    let properror = err.data[prop];
                    validationError = validationError.concat(properror);
                }
                this.validationErrors = validationError;
            });

        }

        public cancelForm() {
            this.product = null;
            this.validationErrors = null;
            this.$state.go('product');
        }
         
        constructor(private $scope,private $stateParams: ng.ui.IStateParamsService,
            private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService, private productServices: ShoppingSmartApp.Services.ProductServices) {

            this.ProductResource = $resource('/api/products/:id');
            var id = $stateParams['id'];
            this.product = this.ProductResource.get({ id: id });
            this.units = this.productServices.getUnits();
        }
    }

    export class DeleteProductController {

        private ProductResource;

        public product;
        public validationErrors;
        public units;

        public delete(id: any) {

            this.ProductResource.remove({ id: id }).$promise.then(() => {
                this.cancelForm();
            }).catch((err) => {
                let validationError = [];
                for (let prop in err.data) {
                    let properror = err.data[prop];
                    validationError = validationError.concat(properror);
                }
                this.validationErrors = validationError;
            });
        }

        public cancelForm() {
            this.product = null;
            this.validationErrors = null;
            this.$state.go('product');
        }

        constructor(private $stateParams: ng.ui.IStateParamsService,
            private $resource: angular.resource.IResourceService, private $http: ng.IHttpService,
            private $state: ng.ui.IStateService, private productServices: ShoppingSmartApp.Services.ProductServices) {

            this.ProductResource = $resource('/api/products/:id');
            var id = $stateParams['id'];
            this.product = this.ProductResource.get({ id: id });
            this.units = this.productServices.getUnits();
        }
    }
}