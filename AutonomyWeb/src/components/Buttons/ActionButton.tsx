import { ReactNode } from "react";

interface ActionButtonProps {
    icon:ReactNode,
    onClick:(e?:any)=>void
}

const ActionButton = (props:ActionButtonProps) => (
    <button onClick={props.onClick} className="hover:text-primary" style={{fontSize: 20}}>{props.icon}</button>
)

export default ActionButton;