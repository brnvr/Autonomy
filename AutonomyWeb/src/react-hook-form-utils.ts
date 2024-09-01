import { FieldError, RegisterOptions } from "react-hook-form";

export function handleErrors(error:FieldError, options:RegisterOptions) {
    if (error) {
        if (error.type == "minLength") {
            if (options.minLength) {
                return `The field must contain at least ${options.minLength} characters.`
            }
        }
    }

    return "";
}