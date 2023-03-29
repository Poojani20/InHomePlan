//$(function () {
//    $("#wizard").steps({
//        headerTag: "h4",
//        bodyTag: "section",
//        transitionEffect: "fade",
//        enableAllSteps: true,
//        transitionEffectSpeed: 500,
//        onStepChanging: function (event, currentIndex, newIndex) {
//            if (newIndex === 1) {
//                $('.steps ul').addClass('step-2');
//            } else {
//                $('.steps ul').removeClass('step-2');
//            }
//            if (newIndex === 2) {
//                $('.steps ul').addClass('step-3');
//            } else {
//                $('.steps ul').removeClass('step-3');
//            }

//            if (newIndex === 3) {
//                $('.steps ul').addClass('step-4');
//                $('.actions ul').addClass('step-last');
//            } else {
//                $('.steps ul').removeClass('step-4');
//                $('.actions ul').removeClass('step-last');
//            }
//            return true;
//        },
//        labels: {
//            finish: "Order again",
//            next: "Next",
//            previous: "Previous"
//        }
//    });
//    // Custom Steps Jquery Steps
//    $('.wizard > .steps li a').click(function () {
//        $(this).parent().addClass('checked');
//        $(this).parent().prevAll().addClass('checked');
//        $(this).parent().nextAll().removeClass('checked');
//    });
//    // Custom Button Jquery Steps
//    $('.forward').click(function () {
//        $("#wizard").steps('next');
//    })
//    $('.backward').click(function () {
//        $("#wizard").steps('previous');
//    })
//    // Checkbox
//    $('.checkbox-circle label').click(function () {
//        $('.checkbox-circle label').removeClass('active');
//        $(this).addClass('active');
//    })
//})

$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    })

});