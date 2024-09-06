import languageDefinitions from "./language.json"
import { setCapitalLetter } from "./string-utils"

type Language = "en"
type MessageType = "actions" | "errors" | "confirmation"

export function getTableInfo(language:Language, key:string):any {
    const lang = languageDefinitions[language]

    if (!lang) {
        throw `Language ${language} not found in language definitions.`
    }

    const tables = lang["tables"]

    if (!tables) {
        throw `Key tables not found for language ${language} in language definitions.`
    }

    const table = tables[key]

    if (!table) {
        throw `Table with key ${key} not found for language ${language} in language definitions.`
    }

    return table
}

export function formatMessage(language:Language, sectionName:MessageType, key:string, ...replaceValues:string[][]):string|any {
    const lang = languageDefinitions[language]

    if (!lang) {
        throw `Language ${language} not found in language definitions.`
    }

    const sections = lang["messages"]

    if (!sections) {
        throw `Key messages not found for language ${language} in language definitions.`
    }

    const section = sections[sectionName]

    if (!section) {
        throw `Section ${sectionName} not found for language ${language} in language definitions.`
    }

    const value = section[key]

    if (!value) {
        throw `Key ${key} not found in section ${sectionName} for language ${language} in language definitions.`
    }

    if (typeof value === "string") {
        let _value = value

        replaceValues.forEach(replaceValue => {
            _value = _value.split(`{${replaceValue[0]}}`).join(replaceValue[1])
        })

        return setCapitalLetter(_value)
    }

    if (typeof value === "object") {
        const _value = {}

        Object.entries(value).forEach((entry:[string, string]) => {
            replaceValues.forEach(replaceValue => {
                entry[1] = entry[1].split(`{${replaceValue[0]}}`).join(replaceValue[1])
            })
            
            _value[entry[0]] = setCapitalLetter(entry[1])
        })

        return _value
    }

    throw "Language definition must be a string or an object."
}
