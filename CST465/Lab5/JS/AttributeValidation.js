
function attributeDataValidation(source, args, type) {
    switch (type) {
        case "String":
            console.log("String Validation.");
            stringAttributeValidation(source, args);
            break;
        case "Integer":
            console.log("Integer Validation.");
            integerAttributeValidation(source, args);
            break;
        case "Decimal":
            console.log("Decimal Validation.");
            decimalAttributeValidation(source, args);
            break;
        case "DateTime":
            console.log("DateTime Validation.");
            DateValidation(source, args);
        default:
            console.log("unable to validate data.");
            break;
    }
}

var StringAttributeMaxLength = 2048;
function stringAttributeValidation(source, args) {
    args.IsValid = false;
    if (args.Value.trim().length <= 0) {
        source.errormessage = 'String attribute must contain some displayable non white-space characters.';
        args.IsValid = false;
    } else if (args.Value.trim().length > StringAttributeMaxLength) {
        source.errormessage = 'String attribute must not contain more than ' + StringAttributeMaxLength + ' characters.';
        args.IsValid = false;
    } else {
        args.IsValid = true;
    }
    return args.IsValid;
}

function integerAttributeValidation(source, args) {
    if (isNumber(args.Value)) {
        args.IsValid = true;
    }
    else {
        source.errormessage = 'Integer attribute must be a base 10 whole number.';
        args.IsValid = false;
    }
}

var AttributeNameMaxLength = 50;
function attributeNameValidation(source, args) {
    args.IsValid = false;
    if (args.Value.trim().length <= 0) {
        source.errormessage = 'Attribute Identifier must contain some displayable non white-space characters.';
        args.IsValid = false;
    } else if (args.Value.trim().length > AttributeNameMaxLength) {
        source.errormessage = 'Attribute Identifier must not contain more than ' + AttributeNameMaxLength + ' characters.';
        args.IsValid = false;
    } else {
        args.IsValid = true;
    }
    return args.IsValid;
}

var EventNameMaxLength = 50;
function eventNameValidation(source, args) {
    args.IsValid = false;
    if (args.Value.trim().length <= 0) {
        source.errormessage = 'Event name identifier must contain some displayable non white-space characters.';
        args.IsValid = false;
    } else if (args.Value.trim().length > EventNameMaxLength) {
        source.errormessage = 'Event name identifier must not contain more than ' + EventNameMaxLength + ' characters.';
        args.IsValid = false;
    } else {
        args.IsValid = true;
    }
}
function decimalAttributeValidation(source, args) {
    if (isFloat(args.Value)) {
        args.IsValid = true;
    }
    else {
        source.errormessage = 'Float attribute must be decimal number.';
        args.IsValid = false;
    }
}
function isFloat(i) {
    var tmp = parseFloat(i)
    return !isNaN(tmp) && tmp == i;
}

function isNumber(i) {
    var tmp = parseInt(i)
    return !isNaN(tmp) && tmp == i;
}

function DateValidation(source, args) {
    args.IsValid = true;
    var regex = new RegExp(/^\d{1,2}\/\d{1,2}\/\d{4} \d{1,2}:\d{1,2}:\d{1,2} [aApP]{1}[mM]{1}$/);
    var input = args.Value.trim();
    if (!regex.test(input)) {
        args.IsValid = false;
        source.errormessage = 'The date is incorrectly formatted, required to be in "M/d/yyyy h:m:s tt" format.';
    } else {
        console.log("Datetime validation regex pass")
        var tmp;
        var timeParts;
        var dateParts = input.split('/');
        if (dateParts.length != 3) {//validate number of date seperators
            args.IsValid = false;
            source.errormessage = 'The date is incorrectly formatted, exactly two "/" date seperators are required to be in M/d/yyyy h:m:s tt format.';
        } else {
            tmp = dateParts[2].split(' ');
            if (tmp.length != 3) {//validate the number of spaced components
                args.IsValid = false;
                source.errormessage = 'The date is incorrectly formatted, containing too many spaces " " whithin the date sperating the year date component from the time and the 12 hr time modifier from the time.';
            } else {
                dateParts[2] = tmp[0];//put the year into the date parts
                timeParts = tmp[1].split(':');
                if (timeParts.length != 3) {//validate number of time seperated components
                    args.IsValid = false;
                    source.errormessage = 'The time component of the date is incorrectly formatted, required to contain exactly two ":" time seperators seperating three numbers hours:minutes:seconds.';
                } else {
                    //validate each date component
                    timeParts[timeParts.length] = tmp[2];
                    var dateErrorMsg = 'The date date component is required to have ';
                    if (!isNumber(dateParts[2])) {
                        args.IsValid = false;
                        dateErrorMsg += 'an integer value for the year component.'
                    } else {//year is determined to be a number
                        if (dateParts[2] < 1800 || dateParts[2] > 9999) {//validate year range
                            args.IsValid = false;
                            dateErrorMsg += 'a year value between 1800 and 9999.';
                        }
                        else {//validate Month
                            if (!isNumber(dateParts[0])) {
                                args.IsValid = false;
                                dateErrorMsg += 'an integer value for the month component.'
                            } else {//month is determined to be a number
                                if (dateParts[0] < 1 || dateParts[0] > 12) {//validate month range
                                    args.IsValid = false;
                                    dateErrorMsg += 'a month value between 1 and 12.';
                                }
                                else {//validate day
                                    if (!isNumber(dateParts[1])) {
                                        args.IsValid = false;
                                        dateErrorMsg += 'an integer value for the day component.'
                                    } else {//day is determined to be a number
                                        var MaxDay = new Date(dateParts[2], dateParts[0], 0).getDate();
                                        if (dateParts[1] < 1 || dateParts[1] > MaxDay) {//validate day range
                                            args.IsValid = false;
                                            dateErrorMsg += 'a day value between 1 and ' + MaxDay + ' for the month of ' + dateParts[0];
                                        }//date component validated
                                    }
                                }
                            }
                        }
                    }//validate Time Component
                    var timeErrorMsg = 'The time component is to have ';
                    if (!args.IsValid) {
                        source.errormessage = dateErrorMsg + '. ';
                    }
                    else {
                        source.errormessage = '';
                    }
                    var timeErrorOccurred = false;
                    if (!isNumber(timeParts[0])) {//check if the rour is a number
                        timeErrorMsg += 'an integer value for the hour component'
                        timeErrorOccurred = true;
                    } else {//hour is determined to be a number
                        if (timeParts[0] < 1 || timeParts[0] > 12) {//validate day range
                            timeErrorMsg += 'a hour value between 1 and 12';
                            timeErrorOccurred = true;
                        }//hour time component validated
                    }
                    //validate minute
                    if (!isNumber(timeParts[1])) {//
                        if (timeErrorOccurred) {
                            timeErrorMsg += ", ";
                        }
                        timeErrorMsg += 'an integer value for the minute component';
                        timeErrorOccurred = true;
                    } else {//minute is determined to be a number
                        if (timeParts[1] < 0 || timeParts[1] > 59) {//validate day range

                            if (timeErrorOccurred) {
                                timeErrorMsg += ", ";
                            }
                            timeErrorMsg += 'a minute value between 1 and 59';
                            timeErrorOccurred = true;
                        }//date component validate
                    }
                    if (!isNumber(timeParts[2])) {//

                        if (timeErrorOccurred) {
                            timeErrorMsg += ", ";
                        }
                        timeErrorMsg += 'an integer value for the seconds component';
                        timeErrorOccurred = true;
                    } else {//minute is determined to be a number
                        if (timeParts[2] < 0 || timeParts[2] > 59) {//validate day range
                            if (timeErrorOccurred) {
                                timeErrorMsg += ", ";
                            }
                            timeErrorMsg += 'a seconds value between 1 and 59';
                            timeErrorOccurred = true;
                        }//date component validate
                    }

                    timeParts[3] = timeParts[3].toUpperCase();
                    if (timeParts[3] == 'AM' || timeParts[3] == 'PM') {

                    }
                    else {
                        if (timeErrorOccurred) {
                            timeErrorMsg += ", ";
                        }
                        timeErrorMsg += 'a time modifier of the value AM or PM';
                        timeErrorOccurred = true;
                    }
                    if (timeErrorOccurred) {
                        args.IsValid = false;

                        source.errormessage += timeErrorMsg + '.';

                    }
                }
            }

        }
    }
}