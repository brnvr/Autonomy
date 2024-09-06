interface Option {
    text:string,
    value:any
}

interface SelectProps {
    defaultValue?:any,
    onChange:(e:any) => void
    options:Option[]
}

const Select = (props:SelectProps) => {
    return (
        <select defaultValue={props.defaultValue} onChange={props.onChange}>
            {props.options.map(option => (
                <option key={option.value} value={option.value}>{option.text}</option>
            ))}
        </select>
    )
}

export default Select