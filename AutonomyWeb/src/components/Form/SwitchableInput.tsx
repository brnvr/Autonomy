import { useEffect, useRef, useState } from "react"

interface SwitchableInputProps {
    type?:string
    defaultValue?:any
    format?:(value:any) => any
    placeholder?:string
    width?:string,
    onSave:(value)=>void
}
const SwitchableInput = (props:SwitchableInputProps) => {
    const [value, setValue] = useState<any>(null)
    const [isEditing, setEditing] = useState<boolean>(false)
    const inputRef = useRef(null)

    const handleFocus = () => {
        setValue(props.defaultValue)
    }

    const handleChange = e => {
        setValue(e.target.value);
    }

    const handleBlur = () => {
        setEditing(false)
        props.onSave(value)
    }

    useEffect(() => {
        if (props.format) {
            setValue(props.format(props.defaultValue))
        } else {
            setValue(props.defaultValue)
        }
    }, [isEditing, props.defaultValue, props.format])

    const handleClick = (e) => {
        setTimeout(() => {
            inputRef.current?.focus(); // Focus on the input after rendering
        }, 0);
        setEditing(true)
    }
    
    return (
        <>
            {!isEditing && <span onClick={handleClick} style={{display:'inline-block', width:props.width, cursor:'pointer'}}>
                {value}
            </span>}
            {isEditing && <input 
                ref={inputRef}
                type={props.type}
                style={{width:props.width}}
                value={value}
                placeholder={props.placeholder ?? ""}
                onFocus={handleFocus}
                onBlur={handleBlur}
                onInput={handleChange}
            />}
        </>
    )
}

export default SwitchableInput