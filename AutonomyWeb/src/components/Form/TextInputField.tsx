import { FieldErrors, Path, PathValue, RegisterOptions, UseFormRegister, UseFormSetValue } from "react-hook-form"
import { handleErrors } from "../../react-hook-form-utils"
import { useEffect } from "react"

interface InputProps<T,F extends Path<T>> {
    label:string
    defaultValue?:PathValue<T,F>
    placeholder?:string
    name:F
    options?:RegisterOptions<T,F>
    register?:UseFormRegister<T>
    setValue:UseFormSetValue<T>
    errors?:FieldErrors<T>
}

const TextInputField = <T,F extends Path<T>,>(props:InputProps<T,F>) => {
    const handleChange = e => {
        props.setValue(props.name, e.target.value);
    }

    useEffect(() => {
        props.setValue(props.name, props.defaultValue)
    }, [props.defaultValue])
    
    return (
        <div className="form-field">
            <label>{props.label}</label>
            <input 
                placeholder={props.placeholder}
                onInput={handleChange}
                {...(props.register && props.name ? props.register(props.name, props.options) : {})}
            />
            {props.errors && props.options && props.name && (
                <span className="error">{handleErrors(props.errors[props.name.toString()], props.options)}</span>
            )}
        </div>
    )
}

export default TextInputField