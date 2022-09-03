$(document).ready(function(){
    $("#checkEye").click(function () {
        if($(this).hasClass('fa-eye')){
            $(".checkEyePwd").attr('type', 'text');
        }else{
            $(".checkEyePwd").attr('type', 'password');
        }
        $(this).toggleClass('fa-eye').toggleClass('fa-eye-slash');
    });
});