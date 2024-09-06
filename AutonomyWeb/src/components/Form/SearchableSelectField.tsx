import { useEffect, useRef, useState } from "react";
import { createPortal } from 'react-dom';

interface SelectOption {
    text: string
    value: string | number
    data?: any
}

interface SelectFieldProps {
    label: string
    options: SelectOption[]
    onInput?: (e:any)=>void
    onLeave?: ()=>void
    onSelect?: (option:SelectOption)=>void
}

const SearchableSelectField = (props: SelectFieldProps) => {
    const [isOpen, setIsOpen] = useState(false)
    const [selected, setSelected] = useState<SelectOption>(null)
    const [value, setValue] = useState<string>(null)
    const [selectBBox, setSelectBBox] = useState<any>(null)
    const optionsContainerRef = useRef<HTMLDivElement>()

    const handleChange = e => {
        setValue(e.target.value)
        props.onInput && props.onInput(e)
    }

    const handleClickOption = (option:SelectOption) => {
        setSelected(option)
        props.onSelect(option)
    }

    const handleFocus = () => {
        setIsOpen(true)
    }

    const handleBlur = () => {
        setValue(null)
        setTimeout(() => {
            setIsOpen(false)
            props.onLeave && props.onLeave()
        }, 100)
    }

    useEffect(() => {
        if (isOpen && optionsContainerRef.current) {
            const rect = optionsContainerRef.current.getBoundingClientRect();

            setSelectBBox({left: rect.left, top: rect.top, width: rect.right-rect.left})
        } else {
            setSelectBBox(null)
        }
    }, [isOpen])

    return (
        <div className="form-field">
            <label>{props.label}</label>
            <div className="search-select">
                <input
                    type="text"
                    value = {value || ""}
                    onInput={handleChange}
                    className = {selected && !isOpen ? "filled" : ""}
                    onFocus={handleFocus}
                    onBlur={handleBlur} 
                    placeholder={selected && !isOpen ? selected.text : (isOpen ? "" : "Select an option")}
                />
                {isOpen && (
                    <div ref={optionsContainerRef} style={{position:'relative'}}>
                        {createPortal(
                            <div className="select-options" style={selectBBox ? {left:selectBBox.left, top:selectBBox.top, width:selectBBox.width} : {display: "none"}}>
                                <ul>
                                    {props.options.map(option => (
                                    <li key={option.value} onClick={() => handleClickOption(option)} >
                                        {option.text}
                                    </li>
                                    ))}
                                </ul>
                            </div>, document.querySelector("#overlay-wrapper")
                        )}
                    </div>
                )}
            </div>
        </div>
    );
};

export default SearchableSelectField;