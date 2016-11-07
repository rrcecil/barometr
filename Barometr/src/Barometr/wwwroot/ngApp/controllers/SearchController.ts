
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public bars;
        public map;
        public mapDiv;
        public lat;
        public lng;
        public geocode;
        public zip;
        public mapOptions;

        constructor(public ngGPlacesAPI, public $http: ng.IHttpService, public $q: ng.IQService, public $scope: ng.IScope, public SearchService: Barometr.Services.SearchService, public $document: ng.IDocumentService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService) {
            this.getBars($stateParams['zipcode']);
        }

        public getBars(zipCode) {
            
            var bars = this.SearchService.getBars(zipCode);
                bars.then((res) => {
                    this.bars = res;
                });
           
        }

        public addBar(bar) {
            bar.disabled = true;
            this.$http.post('api/userBars', bar).then((res) => res);
        }
    }
}






