export function setCapitalLetter(str:string) {
    if (str.length == 0) {
        return str
    }

    const capitalLetter = str[0].toUpperCase()

    if (str.length == 1) {
        return capitalLetter
    }

    return capitalLetter + str.substring(1)
}