import { Post } from "./post";

export interface Group {
    id: number,
    userId: number,
    numberMember: number,
    groupTitle: string,
    description: string,
    posts: Post[],
}