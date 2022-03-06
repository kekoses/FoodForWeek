jQuery.validator.addMethod("case",
    function (value, element, param) {
        let password = String(value);
        let passwordWithoutNumbers = password.replace("[0-9]/g", "");
        let firstCondition = passwordWithoutNumbers.toLowerCase() === passwordWithoutNumbers;
        let secondCondition = passwordWithoutNumbers.toUpperCase() === passwordWithoutNumbers;
        let result = !(firstCondition || secondCondition);
        return result;
    })
jQuery.validator.unobtrusive.adapters.addBool("case");