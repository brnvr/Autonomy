export type TailwindColor = "gray" | "red" | "blue" | "green" | "yellow"

export type TailwindStyleOption = "bg" | "border"

export function getColorClass(type:TailwindStyleOption, color:TailwindColor) {
    switch (color) {
        case 'gray':
            return type + '-gray'
            
        case 'blue':
            return type + '-primary'

        case 'yellow':
            return type + '-meta-6'

        case 'red':
            return type + '-meta-1'

        case 'green':
            return type + '-meta-6'
    }
}