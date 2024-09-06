interface SelectOption {
    name: string
    id: string | number
}

interface SelectFieldProps {
    label: string
    options: SelectOption[]
}

const SelectField = (props: SelectFieldProps) => {
    return (
        <div className="form-field">
            <label>{props.label}</label>
            <select>
                {props.options.map(option => (
                    <option value={option.id}>{option.name}</option>
                ))}
            </select>
        </div>
    );
};

export default SelectField;