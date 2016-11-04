
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

        constructor(public ngGPlacesAPI, public $http: ng.IHttpService, public $q: ng.IQService, public $scope: ng.IScope, public SearchService: Barometr.Services.SearchService, public $document: ng.IDocumentService) {
        }

        public getBars(zipCode) {
            
            this.SearchService.getBars(zipCode);
            console.log(this.SearchService.bars);
           
        }

        public addBar(bar) {
            bar.disabled = true;
            this.$http.post('api/userBars', bar).then((res) => res);
        }
    }
}






