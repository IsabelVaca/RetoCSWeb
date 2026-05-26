
(function () {
    "use strict";
<<<<<<< Updated upstream
    // Funciones para la vista previa de la foto y el mandado de comentarios
=======

    // Validación en el navegador antes del POST EditarPerfil..
    var CFG = {
        maxNombre: 80,
        maxUser: 30,
        maxBio: 500,
        exts: [".jpg", ".jpeg", ".png", ".gif"],
        userRx: /^[a-zA-Z0-9_]+$/
    };

>>>>>>> Stashed changes
    document.addEventListener("DOMContentLoaded", function () {
        var pagina = document.querySelector(".perfil-page");
        if (!pagina) return;

        initVistaPreviaFoto();
        initToggleComentarios();
    });

    // Vista previa al elegir foto en ?tab=editar (no sube nada hasta el POST).
    function initVistaPreviaFoto() {
        var input = document.getElementById("perfil-editar-foto");
        var preview = document.getElementById("perfil-foto-preview");
        if (!input || !preview) return;

        input.addEventListener("change", function () {
            var archivo = input.files && input.files[0];
            if (!archivo) return;

            var urlTemporal = URL.createObjectURL(archivo);
            preview.src = urlTemporal;

            preview.onload = function () {
                URL.revokeObjectURL(urlTemporal);
            };
        });
    }

    // Abre/cierra la caja de comentarios (misma clase .show que Explorar).
    function initToggleComentarios() {
        var botones = document.querySelectorAll(".js-perfil-toggle-comments");

        botones.forEach(function (btn) {
            btn.addEventListener("click", function () {
                var idPanel = btn.getAttribute("data-target");
                var panel = idPanel ? document.getElementById(idPanel) : null;
                if (!panel) return;

                panel.classList.toggle("show");
                var abierto = panel.classList.contains("show");
                btn.setAttribute("aria-expanded", abierto ? "true" : "false");
            });
        });
    }
})();
