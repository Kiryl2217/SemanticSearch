// inputBox - это объект тега input, в который вводится
// строка поиска.
// inputBox в input указан как id, после загрузки документа
// (формирования DOM-структуры), этот id становится объектом.
// inputBox.value - введённая строка.
async function searchQuery() {
    let searchString = inputBox.value;
    divAnswer.innerHTML = "";

    // Запускаем прогресс-бар.
    requestAnimationFrame(callback); 

    // Посылаем запрос.
    const start = Date.now();

    let response = await fetch("https://localhost:7176/Search/SearchAction?userRequest=" + searchString);

    const end = Date.now();

    if (!response.ok) {
        divAnswer.innerHTML = "Ошибка";
        divTime.innerHTML = "";
    } else {
        answer = await response.text();
        divAnswer.innerHTML = answer.replace(/\n/g, '<br>');
        divTime.innerHTML = "Ждали: " + ((end - start) / 1000) + " с";
    }   
}

// Метод потока прогресс-бара.
async function callback(time) {

    // Посылаем запрос.
    let response = await fetch("https://localhost:7176/Search/SearchParam");

    if (!response.ok) {
        divAnswer.innerHTML = "Ошибка";
    } else {
        answer = await response.json();
        progressObj.max = +answer["max"];
        progressObj.value = +answer["value"];
        if (+answer["max"] != 0 && (+answer["value"] > +answer["max"])) return;

    }   





        requestAnimationFrame(callback);
        
}
