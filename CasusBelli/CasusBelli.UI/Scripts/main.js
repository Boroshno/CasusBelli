$(document).ready(function(){
	$('#mapglobal').mouseenter(function(event ) {
		$('#mappanel').clearQueue().stop(true, true).fadeOut( 500 );
	})
	$('#mapglobal').mouseleave(function(event ) {
		$('#mappanel').clearQueue().stop(true, true).fadeIn( 500 );
	})
	
	$('.map').maphilight();
	
	$('.mapglobal').width($('#main').width());
	
	var mainwidth = $('#main').width();
	$('#carouselinner').width(mainwidth + 1);
	$('#carouselinner').css('left', mainwidth + 1);
	
	$('#arrow').click(function() {
		$('#carouselinner').animate({
			'left': 0
		}, 1000, function(){
			
		});
	});
	
	$('.tomap').click(function(){
		$('#carouselinner').animate({
			'left': mainwidth + 1
		}, 1000, function(){
			
		});
	});
	
	$('.bxslider').bxSlider();

    $(window).resize(function() {
        if ($('#main').width() < 700) {
            $('.product').width('47.3%');
        } else {
            $('.product').width('31.3%');
        }
    });

    $('.productname').mouseenter(function(event) {
        $(this).next('.productsubtype').clearQueue().stop(true, true).animate({
            'height': '100%'
        }, 500, function() {
        });
    });

    $('.productcontent').mouseleave(function (event) {
        $(this).children('.productsubtype').clearQueue().stop(true, true).animate({
            'height': '0%'
        }, 500, function () {
        });
    });
});