import { Role } from "./role";

export interface UserDetails {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  address: string;
  note?: string;
  isActive: boolean;
  userRoles: Role[];
  createdAt: Date;
  updatedAt?: Date;
}
