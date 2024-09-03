import { ReactNode } from "react";
import { getColorClass, TailwindColor } from "../../tailwind-utils";

interface ButtonProps {
  content:ReactNode,
  width?:string,
  type?:"button" | "submit" | "reset",
  border?:boolean
  color:TailwindColor
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
    `inline-flex w-full items-center justify-center rounded-md border ${getColorClass('border', props.color)} p-3 text-center font-medium text-primary hover:bg-opacity-90` :
    `inline-flex w-full justify-center rounded ${getColorClass('bg', props.color)} p-3 font-medium hover:bg-opacity-90 ${getTextClass(props.color)}`

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