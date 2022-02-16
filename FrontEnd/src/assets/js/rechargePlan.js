$(document).ready(function(){
  var getRechargePlan = JSON.parse(localStorage.getItem("Recharge_Plan"));
    if(getRechargePlan == '' || getRechargePlan == null)
    {
      $.getJSON('../../assets/json/rechargePlan.json', function(rechargePlan) {
        localStorage.setItem("Recharge_Plan",JSON.stringify(rechargePlan));
        appendData(rechargePlan);
      });
    }
    else{
      appendData(getRechargePlan);
    }
    
  function appendData(data)
  {
    $.each(data.plan, function( key, item ) {
      var row = $('<div class="card border-dark m-3"> <div class="card-body d-flex flex-row justify-content-around"> <div class="planPrice text-center"> <h5 class="card-title"> ₹'
                      +item.price+ 
                  '</h5></div><div class="card-text row"><p class="col-4">'+item.call+'</p><p class="col-4">'+item.data+'</p><p class="col-4">'+item.totalData+'</p><p class="col-4">'+item.validity+'</p><p class="col-4">'+item.status+'</p><p class="col-4">'+item.serviceValidity+'</p></div><a href="#" class="btn rounded-pill btn-danger text-center p-4">buy pack</a></div></div>');
      
      $('.rechargePlan').append(row);
      $('.card-title').css({"margin": "0",
                            "font-size": "40px",
                            "top": "0",
                            // "line-height": "56px",
                            "color":"#fff"});
      $('.planPrice').css({"background-color": "#2f3043",
                           "padding":"0",
                           "margin":"0"})
      $('.card-body').css({"background-color": "#2f3043"})
      $('.card-text').css({"color": "#fff"})
  });    
  }
});

/* <div class="card  m-3">
            <div class="card-body d-flex flex-row justify-content-between">
              <h5 class="card-title">Card title</h5>
              <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
              <a href="#" class="btn btn-primary">Go somewhere</a>
            </div>
          </div> */
