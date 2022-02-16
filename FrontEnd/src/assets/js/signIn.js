$(document).ready(function(){
    $('#sendOtpBtn').click(function(){
        $('#errorMsg').html("");
        var mobileNo =  $('#mobile').val();

        if(mobileNo == "")
        {
            $('#errorMsg').html("Enter Mobile Number");
        }
        else
        {
            if(isNaN(mobileNo))
            {
                $('#errorMsg').html("Enter Only Numbers");
            }
            if(mobileNo.length != 10)
            {
                $('#errorMsg').html("Enter 10 Digit Numbers");
            }
            $.getJSON('../../assets/json/userData.json', function(signInData) {
                $.each(signInData.users, function( key, item ) {
                    if(item.mobileNo == mobileNo)
                    {
                        alert("Otp : "+item.otp);
                        window.location = "http://127.0.0.1:5500/";
                    }
                });
                $('#errorMsg').html("Enter Currect Mobile Number"); 
            });
            
        }
    })

    
})