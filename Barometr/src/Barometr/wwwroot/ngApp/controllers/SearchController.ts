
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public message = 'Hello from the about page!';
        public options;
        public neighborhoods;
        public bars;

        constructor(public ngGPlacesAPI) {
            this.options = {
                types: ['establishment']
            };
            this.neighborhoods = {
                heights: { latitude: 29.790128, longitude: -95.402796 },
                riceVillage: { latitude: 29.715253, longitude: -95.418873 },
                pearland: { latitude: 29.543877, longitude: -95.387624 }
            };

            
        }

        public getBars(hood) {
            var hoodObj = JSON.parse(hood);

            this.ngGPlacesAPI.nearbySearch({ latitude: hoodObj.latitude, longitude: hoodObj.longitude }).then((res) => {
                this.bars = "";
                this.bars = res;
            });
        }

        public logThis() {
            console.log(this.result);
        }

    }
}





