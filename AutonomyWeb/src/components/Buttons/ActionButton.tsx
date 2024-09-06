import { IconType } from "react-icons";

interface ActionButtonProps {
    icon:IconType,
    onClick:(e?:any)=>void
}

const ActionButton = (props:ActionButtonProps) => (
    <button type="button" onClick={props.onClick} className="hover:text-primary" style={{fontSize: 20}}>{<props.icon />}</button>
)

export default ActionButton;