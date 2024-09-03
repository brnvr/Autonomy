import { FieldError, RegisterOptions } from "react-hook-form";

export function handleErrors(error:FieldError, options:RegisterOptions) {
    if (error) {
        switch(error.type) {
            case 'required':
                if (options.required) {
                    return 'The field is required.'
                }

                break

            case 'minLength':
                if (options.minLength) {
                    return `The field must contain at least ${options.minLength} characters.`
                }

                break
        }
    }

    return "";
}