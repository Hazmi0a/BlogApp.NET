
function filter() {
                var text = $("#search").val().toLowerCase();
                $(".card").each(function() {
                    var card = $(this);
                    var id = card.find("#title").text().replace(/\(|\)/g, '').toLowerCase();

                    if(id.indexOf(text) > -1) {
                        // to show the result of hidden
                        //card.show();

                        card.css('opacity','1');
                    } else {
                        //to hide anything other thant the result
                        //card.hide();

                        card.css('opacity','0.1');
                    }
                    
                });
            }

$(document).ready(function(){
    $('.button-collapse').sideNav({

        edge: 'right', // Choose the horizontal origin
        draggable: true, // Choose whether you can drag to open on touch screens

    });
    
  });
   
