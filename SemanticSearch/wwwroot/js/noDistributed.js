// inputBox - это объект тега input, в который вводится
// строка поиска.
// inputBox в input указан как id, после загрузки документа
// (формирования DOM-структуры), этот id становится объектом.
// inputBox.value - введённая строка.
async function searchQuery1() {
    let searchString = inputBox1.value;
    divAnswer1.innerHTML = "";

    // Запускаем прогресс-бар.
    requestAnimationFrame(callback1);

    // Посылаем запрос.
    const start = Date.now();

    let response = await fetch("https://localhost:7176/NoDistributed/SearchAction?userRequest=" + searchString);

    const end = Date.now();

    if (!response.ok) {
        divAnswer1.innerHTML = "Ошибка";
        divTime1.innerHTML = "";
    } else {
        answer = await response.text();
        divAnswer1.innerHTML = answer;
        divTime1.innerHTML = "Ждали: " + ((end - start) / 1000) + " с";
    }
}

// Метод потока прогресс-бара.
async function callback1(time) {

    // Посылаем запрос.
    let response = await fetch("https://localhost:7176/NoDistributed/SearchParam");

    if (!response.ok) {
        divAnswer1.innerHTML = "Ошибка";
    } else {
        answer = await response.json();
        progressObj1.max = +answer["max"];
        progressObj1.value = +answer["value"];
        if (+answer["max"] != 0 && (+answer["value"] > +answer["max"])) return;

    }





    requestAnimationFrame(callback1);

}
