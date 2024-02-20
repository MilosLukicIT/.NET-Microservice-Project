import { Contributor } from "./contributor";

export class TaskDialogData {
    title : string;
    description : string;
    assignee : Contributor;
    contributors: Contributor[]
}