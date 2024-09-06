import { ReactNode } from "react";
import { getColorClass, TailwindColor } from "../../tailwind-utils";

interface ButtonProps {
  content:ReactNode,
  width?:string,
  type?:"button" | "submit" | "reset",
  border?:boolean
  disabled?:boolean
  className?:string
  color:TailwindColor
  animation?:"expand-height" | "evidence"
  onClick?:(e?:any)=>void
}

const getTextClass = (color:TailwindColor) => {
  if (color == "gray") {
    return "text-black"
  }

  return "text-gray"
}

const Button = (props:ButtonProps) => {
  const className = props.border ?
    `btn border ${getColorClass('border', props.color)} ${getColorClass('text', props.color)} ${props.animation && ("anim-" + props.animation)}` :
    `btn ${getColorClass('bg', props.color)} ${getTextClass(props.color)} ${props.animation && ("anim-" + props.animation)}`

  return (
      <button
        type={props.type ?? "button"}
        className={className}
        disabled={props.disabled}
        style={{width:props.width}}
        onClick={props.onClick}
      >
        {props.content}
      </button>
  )
}

export default Button;