$(document).ready(function () {
    $('.star-rating i').hover(function () {
        $(this).prevAll().removeClass('fa-star-o fa-star-half-full').addClass('fa-star');
        $(this).removeClass('fa-star-o fa-star-half-full').addClass('fa-star');
        $(this).nextAll().addClass('fa-star-o').removeClass('fa-star fa-star-half-full');
    }, function () {
        //
    });

    $('.star-rating').hover(function () {
        //
    }, function () {
        $(this).children('i').each(function () {
            $(this).removeClass('fa-star-o fa-star-half-full')
            $(this).attr('class', $(this).attr('data-prev-rating-class'));
        });
    });
});