window.getArraySum = (numberArray) => {
    return numberArray.reduce((a, b) => a + b, 0);
}

window.showValue = (element) => {
    alert('Your name is :' + element.value);
}

window.printMessageToConsole = () => {
    DotNet.invokeMethodAsync('BlazorFundamentals', 'PrintMessage')
        .then(data => {
            console.log(data);
        });
}

function displayMessageCallerJS(value) {
    DotNet.invokeMethodAsync('BlazorFundamentals', 'DisplayMessageCaller', value);
}