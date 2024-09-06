import { FieldErrors, Path, PathValue, RegisterOptions, UseFormClearErrors, UseFormRegister, UseFormResetField, UseFormSetValue } from "react-hook-form"
import { handleErrors } from "../../react-hook-form-utils"
import { useEffect } from "react"

interface InputFieldProps<T,F extends Path<T>> {
    label:string
    defaultValue?:PathValue<T,F>
    placeholder?:string
    name:F
    type?:string
    options?:RegisterOptions<T,F>
    register?:UseFormRegister<T>
    setValue:UseFormSetValue<T>
    errors?:FieldErrors<T>
}

const InputField = <T,F extends Path<T>,>(props:InputFieldProps<T,F>) => {
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
                type={props.type || "text"}
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

export default InputField