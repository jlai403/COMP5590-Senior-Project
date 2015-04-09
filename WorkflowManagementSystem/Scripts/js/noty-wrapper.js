function showSuccessNotification(text) {
    noty({
        layout: "center",
        theme: "relax",
        type: "success",
        text: text,
        timeout: 2000,
        buttons: false,
        animation: {
            open: "animated fadeIn", // Animate.css class names
            close: "animated fadeOut", // Animate.css class names
            easing: "swing",
            speed: 500
        }
    });
}

function showErrorNotification(text) {
    noty({
        layout: "center",
        theme: "relax",
        type: "error",
        text: text,
        timeout: 2000,
        buttons: false,
        animation: {
            open: "animated fadeIn", // Animate.css class names
            close: "animated fadeOut", // Animate.css class names
            easing: "swing",
            speed: 500
        }
    });
}
