(function () {
    "use strict";

    // Validación en el navegador antes del POST EditarPerfil.
    // Funciones para la vista previa de la foto y el mandado de comentarios
    var CFG = {
        maxNombre: 80,
        maxUser: 30,
        maxBio: 500,
        exts: [".jpg", ".jpeg", ".png"],
        userRx: /^[a-zA-Z0-9_]+$/
    };


    document.addEventListener("DOMContentLoaded", function () {
        var page = document.querySelector(".perfil-page");
        if (!page) return;

        initEditar();
        initComentarios();
        page.addEventListener("click", onClickTarjeta);
    });

    function initEditar() {
        var form = document.querySelector(".perfil-editar-form");
        if (!form) return;

        var nombre = document.getElementById("perfil-editar-nombre");
        var user = document.getElementById("perfil-editar-user");
        var bio = document.getElementById("perfil-editar-bio");
        var foto = document.getElementById("perfil-editar-foto");
        var errs = document.getElementById("perfil-editar-errores-cliente");
        var preview = document.getElementById("perfil-foto-preview");
        var aviso = document.getElementById("perfil-formatos-foto");

        if (nombre) nombre.maxLength = CFG.maxNombre;
        if (user) user.maxLength = CFG.maxUser;
        if (bio) {
            bio.maxLength = CFG.maxBio;
            var cnt = document.getElementById("contador-chars");
            if (cnt) {
                var up = function () { cnt.textContent = bio.value.length; };
                bio.addEventListener("input", up);
                up();
            }
        }
        if (foto) {
            foto.accept = CFG.exts.join(",");
            foto.addEventListener("change", function () {
                var e = validarFoto(foto);
                if (e) { showErrs(errs, [e]); foto.value = ""; return; }
                hideErrs(errs);
                if (preview && foto.files[0]) preview.src = URL.createObjectURL(foto.files[0]);
            });
        }
        if (aviso) aviso.textContent = "Formatos: " + CFG.exts.join(", ") + ". Sin archivo nuevo se mantiene la foto.";

        form.addEventListener("submit", function (ev) {
            var lista = validarEditar(nombre, user, bio, foto);
            if (lista.length) { ev.preventDefault(); showErrs(errs, lista); }
            else hideErrs(errs);
        });
    }

    function validarEditar(nombre, user, bio, foto) {
        var e = [];
        var n = nombre?.value.trim() || "";
        var u = user?.value.trim() || "";
        var b = bio?.value || "";
        if (!n) e.push("El nombre es obligatorio.");
        else if (n.length > CFG.maxNombre) e.push("Nombre demasiado largo.");
        if (!u) e.push("El usuario es obligatorio.");
        else if (!CFG.userRx.test(u)) e.push("Solo letras, números y guion bajo.");
        if (b.length > CFG.maxBio) e.push("Biografía demasiado larga.");
        var f = validarFoto(foto);
        if (f) e.push(f);
        return e;
    }

    function validarFoto(input) {
        if (!input?.files?.[0]) return null;
        var name = input.files[0].name;
        var i = name.lastIndexOf(".");
        var ext = i >= 0 ? name.slice(i).toLowerCase() : "";
        return CFG.exts.includes(ext) ? null : "Extensiones no permitidas: " + CFG.exts.join(", ");
    }

    function showErrs(box, list) {
        if (!box) return;
        box.innerHTML = list.map(function (t) { return "<div>" + t + "</div>"; }).join("");
        box.classList.remove("d-none");
    }

    function hideErrs(box) {
        if (!box) return;
        box.innerHTML = "";
        box.classList.add("d-none");
    }

    function initComentarios() {
        document.querySelectorAll(".js-perfil-toggle-comments").forEach(function (btn) {
            btn.addEventListener("click", function () {
                var panel = document.getElementById(btn.getAttribute("data-target"));
                if (!panel) return;
                panel.classList.toggle("show");
                btn.setAttribute("aria-expanded", panel.classList.contains("show"));
            });
        });
    }

    // Like y guardar: cambio visual local.
    function onClickTarjeta(ev) {
        var like = ev.target.closest(".like-btn");
        if (like) { toggleStat(like, "liked", "bi-heart", "bi-heart-fill", ".like-count"); return; }
        var save = ev.target.closest(".save-btn");
        if (save) toggleStat(save, "saved", "bi-bookmark", "bi-bookmark-fill", ".save-count");
    }

    function toggleStat(btn, cls, iconOff, iconOn, countSel) {
        btn.classList.toggle(cls);
        var icon = btn.querySelector("i");
        var count = btn.querySelector(countSel);
        if (!icon || !count) return;
        var n = parseInt(count.textContent, 10) || 0;
        var on = btn.classList.contains(cls);
        icon.classList.toggle(iconOn, on);
        icon.classList.toggle(iconOff, !on);
        count.textContent = on ? n + 1 : Math.max(0, n - 1);
        btn.setAttribute("aria-pressed", on);
    }
})();
