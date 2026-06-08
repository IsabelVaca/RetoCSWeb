document.addEventListener("DOMContentLoaded", function () {

//se obtienen los elementos del formulario (crear prompt) por su id
    const titleInput = document.getElementById("Title");
    const categoriaInput = document.getElementById("IdCategoria");
    const descriptionInput = document.getElementById("Description");
    const promptInput = document.getElementById("Prompt");
    const generateButton = document.getElementById("generateButton");
    const clearButton = document.getElementById("clearButton");

//revisa que todas las boxes tengan información
//si alguna box no tiene info deja el boton desactivado
    function revisarFormulario() {
        generateButton.disabled =
            titleInput.value.trim() === "" ||
            categoriaInput.value.trim() === "" ||
            descriptionInput.value.trim() === "" ||
            promptInput.value.trim() === "";
    }

//cada vez que el usuario cambia la categoría, se revisa el formulario
    titleInput.addEventListener("input", revisarFormulario);
    categoriaInput.addEventListener("change", revisarFormulario);
    descriptionInput.addEventListener("input", revisarFormulario);
    promptInput.addEventListener("input", revisarFormulario);

//limpia las boxes cuando el usuario presiona el boton de limpiar
    clearButton.addEventListener("click", function () {
        titleInput.value = "";
        categoriaInput.value = "";
        descriptionInput.value = "";
        promptInput.value = "";

        revisarFormulario();
    });

    
    revisarFormulario();
});