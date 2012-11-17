$(function () {

    $('.notifications-link').click(function (e) {
        $(this).siblings('.notifications').toggle();
        e.preventDefault();
    });
	
    $('.tool-select-trigger').click(function (e) {
        $(this).parents('.header-tools').find('.tool-select').toggle();
        e.preventDefault();
    });
    $('.tool-select a').click(function (e) {
        activity_type = $(this).attr('class');
        new_title = $(this).text();
        toggle_element = $(this).parents('.header-tools').find('.tool-select-trigger');
        default_title = toggle_element.text();

        toggle_element.text(new_title);
        
        if (activity_type == 'all-activity-types') {
            $('#the_feed li').show();
        } else {
            $('#the_feed li').fadeOut(300).delay(300);
            $('li.' + activity_type + '').fadeIn(300).delay(300);
        }
        $(this).parents('.tool-select').toggle();
        e.preventDefault();
    });

    // TEMPORARY JS FOR COMMENTS
    $('.open-comments').click(function () {
        $(this).parents('.item-meta').find('.comments-container').toggle();
        $(this).toggleClass('toggled');
        preventDefault();
    });


	
});