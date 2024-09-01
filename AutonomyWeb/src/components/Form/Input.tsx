import { FieldErrors, Path, RegisterOptions, UseFormRegister } from "react-hook-form"
import { handleErrors } from "../../react-hook-form-utils"

interface InputProps<T,F extends Path<T>> {
    defaultValue?:string
    placeholder?:string,
    fieldName?:F,
    options?:RegisterOptions<T,F>
    register?:UseFormRegister<T>,
    errors?:FieldErrors<T>,
}

const Input = <T,F extends Path<T>,>(props:InputProps<T,F>) => {
    return (
        <div className="form-field">
            <label>Description</label>
            <input 
                defaultValue={props.defaultValue}
                placeholder={props.placeholder}
                {...(props.register && props.fieldName ? props.register(props.fieldName, props.options) : {})}
            />
            {props.errors && props.options && props.fieldName && (
                <span className="error">{handleErrors(props.errors[props.fieldName.toString()], props.options)}</span>
            )}
        </div>
    )
}

export default Input