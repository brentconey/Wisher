$(function () {

    $('.call-dibs').click(function (e) {
        $(this).animate({
            opacity: 0,
            width: 0
        }, 500);
        $(this).siblings('.item-dibs,.dibs-called-icon').fadeIn(500);
        e.preventDefault();
    });

// manages the header search effects and functionality
    $('.activate-search').click(function (e) {
        $(this).toggleClass('toggled');
        $('#global_search input[type="text"]').focus();
        e.preventDefault();
    });
    $('#global_search input[type="text"]').blur(function () {
        $('.activate-search').removeClass('toggled');
    });
    $('#global_search input[type="text"]').focus(function () {
        $('.activate-search').addClass('toggled');
    });


    $('.activate-panel').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        var panelToOpen = $(this).attr('href');
        if ($(panelToOpen).hasClass('overlay')) {
            $('body').append('<div id="ui_blocker"></div>');
        }
        $('.activate-panel').removeClass('toggled');
        if ($(panelToOpen).is(':visible')) {
            $(panelToOpen).hide();
        } else {
            $('.panel').fadeOut(300);
            $(panelToOpen).show();
            $(this).addClass('toggled');
        }
    });

    $('.panel').click(function (e) {
        e.stopPropagation();
    });

    $('.close-overlay').click(function () {
        $('.activate-panel').removeClass('toggled');
        $('.panel').hide();
        $('#ui_blocker').remove();
    });

    $(document).on("click", "html", function (e) {
        $.each($('.panel'), function () {
            if ($(this).is(':visible')) {
                if ($(this).hasClass('overlay')) {
                    $('#ui_blocker').remove();
                }
                $(this).hide();

                $('.activate-panel').removeClass('toggled');
            }
        });
    });


    $('.panel-tabs-container .panel-tab').hide();
    $('.panel-tabs-container .panel-tab:first').show();
    $('.panel-tabs li:first').addClass('toggled');
    $('.panel-tabs li a').click(function (e) {
        $('.panel-tabs li').removeClass('toggled');
        $(this).parents('li').addClass('toggled');
        var currentTab = $(this).attr('href');
        $('.panel-tabs-container .panel-tab').hide();
        $(currentTab).show();
        e.preventDefault();
    });


    $('.tabs .tab').hide();
    $('.tabs .tab:first').show();
    $('.tabs ol li:first').addClass('active');

    $('.tabs h3 a').click(function () {
        $('.tabs ol li').removeClass('active');
        $(this).parents('li').addClass('active');
        var currentTab = $(this).attr('href');
        $('.tabs .tab').hide();
        $(currentTab).show();
        return false;
    });

    /* maybe later
    $('.thing').click(function () {
        var thing_url = $(this).find('.thing-link').attr('href');
        window.location.href = thing_url;
    });
    */
	
    $('.header-with-tools .panel a').click(function (e) {
        activity_type = $(this).attr('class');
        new_title = $(this).text();
        toggle_element = $(this).parents('.header-tools').find('.activate-panel');
        default_title = toggle_element.text();

        toggle_element.text(new_title);
        
        if (activity_type == 'all-activity-types') {
            $('#the_feed li').show();
        } else {
            $('#the_feed li').fadeOut(300).delay(300);
            $('li.' + activity_type + '').fadeIn(300).delay(300);
        }
        $(this).parents('.panel').toggle();
        e.preventDefault();
    });

    // TEMPORARY JS FOR COMMENTS
    $('.open-comments').click(function () {
        $(this).parents('.item-meta').find('.comments-container').toggle();
        $(this).toggleClass('toggled');
        preventDefault();
    });


	
});
//Helper function for checking inputs for values
function HasValue(value) {
    if (value) {
        return false;
    } else {
        return true;
    }
}