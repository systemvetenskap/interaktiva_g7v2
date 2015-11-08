//Timern följer med när man scrollar ner
$(document).ready(function () {
    var top = $('#timerbox').offset().top - parseFloat($('#timerbox').css('marginTop').replace(/auto/, 0));
    $(window).scroll(function (event) {
        var y = $(this).scrollTop();
        if (y >= top) {
            var difference = y - top;
            $('#timerbox').css("top", difference);
        }
    });
});