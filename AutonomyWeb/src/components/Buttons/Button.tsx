import { ReactNode } from "react";
import { Link } from "react-router-dom";

interface ButtonProps {
  content:string,
  width?:string,
  type?:"button" | "submit" | "reset",
  border?:boolean
  onClick?:()=>void
}



const Button = (props:ButtonProps) => {
  const className = props.border ?
    "inline-flex w-full items-center justify-center rounded-md border border-primary p-3 text-center font-medium text-primary hover:bg-opacity-90" :
    "inline-flex w-full justify-center rounded bg-primary p-3 font-medium text-gray hover:bg-opacity-90"

  return (
      <button
        type={props.type ?? "button"}
        className={className}
        style={{width:props.width}}
        onClick={props.onClick}
      >
        {props.content}
      </button>
  )
}

export default Button;