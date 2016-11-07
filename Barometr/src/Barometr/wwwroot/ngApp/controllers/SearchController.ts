
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
        public favs;

        constructor(public ngGPlacesAPI, public $http: ng.IHttpService, public $q: ng.IQService, public $scope: ng.IScope, public SearchService: Barometr.Services.SearchService, public $document: ng.IDocumentService, public $state: ng.ui.IStateService, public $stateParams: ng.ui.IStateParamsService) {
            this.getBars($stateParams['zipcode']);
            this.$http.get('api/favoriteBar/favoriteBars').then((res) => {
                this.favs = res.data;
                
            });
        }

        public getBars(zipCode) {

            var bars = this.SearchService.getBars(zipCode);
            bars.then((res) => {
                this.bars = res;
                this.bars.forEach((e) => {
                    for (let i = 0; i < this.favs.length; i++) {
                        if (e['googleBarId'] == this.favs[i]['googleBarId']){
                            e.disabled = true;
                        }
                    }
                });
            });

        }

        public addBar(bar) {
            console.log(bar);
            bar.disabled = true;
            this.$http.post('api/favoriteBar', bar).then((res) => res);
        }
    }
}






