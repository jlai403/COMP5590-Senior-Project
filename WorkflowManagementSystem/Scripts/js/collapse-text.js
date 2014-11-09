(function($) {
	$.fn.addReadMore = function() {
		var element = $(this);
		
		if (element.height() < 16) return;
		
		element.addClass("collapsed");
		element.css("height","30px");
		var readMore = $("<span class='show-more'>read more ...</span>");
		readMore.insertAfter(element);

        element.parent().on("click", ".show-more", function() {
            $(this).expandText();
        });

        element.parent().on("click", ".show-less", function() {
            $(this).collapseText();
        });
	}
	
	$.fn.expandText = function() {
		var actionElement = $(this);
		actionElement.removeClass("show-more");
		actionElement.addClass("show-less");
		actionElement.text("show less ...");
		
		var textToExpand = actionElement.prev();
		var height = textToExpand.height();
		var heightToExpand = textToExpand.css("height","auto").height();
		textToExpand.css("height",height).height();
		textToExpand.animate({height: heightToExpand}).removeClass("collapsed");
	}
	
	$.fn.collapseText = function() {
		var actionElement = $(this);
		actionElement.removeClass("show-less");
		actionElement.addClass("show-more");
		actionElement.text("read more ...");
		
		var textToCollapse = actionElement.prev();
		textToCollapse.animate({height:"30"}).addClass("collapsed");
	}
}(jQuery));