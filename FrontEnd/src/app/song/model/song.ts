export interface Song{

    songId :number,
    songName : string,
    songPath : string,
    songPoster:string,
    songLyrics : string,
    songGeneration : string,
    songType : string,
    songDescription : string,
    likes:number,
    categoryId: number,
    userId:number,
    singers:number[]
}