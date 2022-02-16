// $(document).ready(function(){
//     $('input').click(function(){
//         $('span').remove();
//         var a = $(this).val();
//         $(this).parent().after($('<span>', {
//                     style : "padding-left : 10px",
//                     text: "You have selected : " + a }));
//     });
// });

// $('.carousel').bootstrap.Carousel({
//     interval: 3000,
//     keyboard: true,
//     pause: 'hover',
//     warp: 'true'
//   });

  var myCarousel = document.querySelector('.carousel')
  var carousel = new bootstrap.Carousel(myCarousel, {
    interval: 2000,
    keyboard: true,
    pause: 'hover',
    warp: 'true'
  })

$(document).ready(function(){
    // $('input').click(function(){
    //     $(this).css({"background-color":"#2f3043"});
    // });

    
});
