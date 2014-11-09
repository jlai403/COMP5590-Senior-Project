var errors = []

function addError(errorText, inputName) {
	errors.push($("<li>" + errorText + "</li>"));
	if (inputName != null) {
		$("input[name='" + inputName + "']").addClass("error-highlight");
		$("select[name='" + inputName + "']").addClass("error-highlight");
		$("textarea[name='" + inputName + "']").addClass("error-highlight");
	}
}

function printErrors() {
	$(".errors").append(errors);
}

function clearErrors() {
	errors.length = 0;
	$(".errors").empty();
}

function hasErrors() {
	return errors.length > 0;
}

function clearHighlightsOnFocus() {
	$("input").focus(function() {
		$(this).removeClass("error-highlight");
	});
	$("select").focus(function() {
		$(this).removeClass("error-highlight");
	});
	$("textarea").focus(function() {
		$(this).removeClass("error-highlight");
	});
}