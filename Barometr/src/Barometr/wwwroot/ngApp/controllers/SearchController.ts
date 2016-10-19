
namespace Barometr.Controllers {

    export class SearchController {
        public result;
        public message = 'Hello from the about page!';
        public options;
        constructor(public ngGPlacesAPI) {
            this.options = {
                types: ['establishment']
            };
        }

        public logThis() {
            console.log(this.result);
        }

    }
}





