using System;
using ToyVM.bytecodes;
namespace ToyVM
{
	/// <summary>
	/// Summary description for ByteCodeFactory.
	/// </summary>
	public class ByteCodeFactory
	{
		public ByteCodeFactory()
		{
			
		}

		public static ByteCode readByteCode(MSBBinaryReaderWrapper reader,ConstantPoolInfo[] pool,int offset)
		{
			byte code = reader.ReadByte();

			ByteCode byteCode = null;
			switch (code)
			{
			case 0x00:  {byteCode = new ByteCode_nop(code,reader,pool); break; }
			case 0x01:  {byteCode = new ByteCode_aconst_null(code,reader,pool); break; }
			case 0x02:  {byteCode = new ByteCode_iconst(code,reader,pool,-1); break; }
			case 0x03:  {byteCode = new ByteCode_iconst(code,reader,pool,0); break; }
			case 0x04:  {byteCode = new ByteCode_iconst(code,reader,pool,1); break; }
			case 0x05:  {byteCode = new ByteCode_iconst(code,reader,pool,2); break; }
			case 0x06:  {byteCode = new ByteCode_iconst(code,reader,pool,3); break; }
			case 0x07:  {byteCode = new ByteCode_iconst(code,reader,pool,4); break; }
			case 0x08:  {byteCode = new ByteCode_iconst(code,reader,pool,5); break; }
			case 0x09:  {byteCode = new ByteCode_lconst(code,reader,pool,0); break; }
			case 0x0A:  {byteCode = new ByteCode_lconst(code,reader,pool,1); break; }
			case 0x0B:  {byteCode = new ByteCode_fconst(code,reader,pool,0); break; }
			case 0x0C:  {byteCode = new ByteCode_fconst(code,reader,pool,1); break; }
			case 0x0D:  {byteCode = new ByteCode_fconst(code,reader,pool,2); break; }
			case 0x0E:  {byteCode = new ByteCode_dconst(code,reader,pool,0); break; }
			case 0x0F:  {byteCode = new ByteCode_dconst(code,reader,pool,1); break; }
			case 0x10: 	{byteCode = new ByteCode_bipush(code,reader,pool);break; }
			case 0x11: 	{byteCode = new ByteCode_sipush(code,reader,pool);break; }
			case 0x12: 	{byteCode = new ByteCode_ldc(code,reader,pool);break; }
			case 0x13:  {byteCode = new ByteCode_ldc_w(code,reader,pool); break; }
			case 0x14:  {byteCode = new ByteCode_ldc2_w(code,reader,pool); break; }
			case 0x15:  {byteCode = new ByteCode_iload(code,reader,pool); break; }
			case 0x16:  {byteCode = new ByteCode_lload(code,reader,pool); break; }
			case 0x19:  {byteCode = new ByteCode_aload0(code,reader,pool); break; }
			case 0x1A:  {byteCode = new ByteCode_iload(code,reader,pool,0);break; }
			case 0x1B:  {byteCode = new ByteCode_iload(code,reader,pool,1);break; }
			case 0x1C:  {byteCode = new ByteCode_iload(code,reader,pool,2);break; }			
			case 0x1D:  {byteCode = new ByteCode_iload(code,reader,pool,3);break; }
			case 0x1E: 	{byteCode = new ByteCode_lload(code,reader,pool,0);break; }	
			case 0x1F: 	{byteCode = new ByteCode_lload(code,reader,pool,1);break; }
			case 0x20: 	{byteCode = new ByteCode_lload(code,reader,pool,2);break; }
			case 0x21: 	{byteCode = new ByteCode_lload(code,reader,pool,3);break; }
			case 0x22: 	{byteCode = new ByteCode_fload(code,reader,pool,0);break; }
			case 0x23: 	{byteCode = new ByteCode_fload(code,reader,pool,1);break; }
			case 0x24: 	{byteCode = new ByteCode_fload(code,reader,pool,2);break; }
			case 0x25: 	{byteCode = new ByteCode_fload(code,reader,pool,3);break; }
			case 0x26: 	{byteCode = new ByteCode_fload(code,reader,pool,0);break; }
			case 0x27: 	{byteCode = new ByteCode_fload(code,reader,pool,1);break; }
			case 0x28: 	{byteCode = new ByteCode_fload(code,reader,pool,2);break; }
			case 0x2A: 	{byteCode = new ByteCode_aload0(code,reader,pool,0);break; }
			case 0x2B:  {byteCode = new ByteCode_aload0(code,reader,pool,1);break; }
			case 0x2C:  {byteCode = new ByteCode_aload0(code,reader,pool,2);break; }
			case 0x2D:  {byteCode = new ByteCode_aload0(code,reader,pool,3);break; }
			case 0x2E:  {byteCode = new ByteCode_iaload(code,reader,pool);break; }
			case 0x32:  {byteCode = new ByteCode_aaload(code,reader,pool);break; }
			case 0x33:  {byteCode = new ByteCode_baload(code,reader,pool);break; }
			case 0x34:  {byteCode = new ByteCode_caload(code,reader,pool);break; }
			case 0x36:  {byteCode = new ByteCode_istore(code,reader,pool);break; }
			case 0x37:  {byteCode = new ByteCode_lstore(code,reader,pool);break; }
			case 0x3A:  {byteCode = new ByteCode_astore(code,reader,pool);break; }
			case 0x3B:  {byteCode = new ByteCode_istore(code,reader,pool,0);break; }
			case 0x3C:  {byteCode = new ByteCode_istore(code,reader,pool,1);break; }
			case 0x3D:  {byteCode = new ByteCode_istore(code,reader,pool,2);break; }
			case 0x3E:  {byteCode = new ByteCode_istore(code,reader,pool,3);break; }
			case 0x3F:  {byteCode = new ByteCode_lstore(code,reader,pool,0);break; }
			case 0x40:  {byteCode = new ByteCode_lstore(code,reader,pool,1);break; }
			case 0x41:  {byteCode = new ByteCode_lstore(code,reader,pool,2);break; }
			case 0x42:  {byteCode = new ByteCode_lstore(code,reader,pool,3);break; }
			case 0x43:  {byteCode = new ByteCode_fstore(code,reader,pool,0);break; }
			case 0x44:  {byteCode = new ByteCode_fstore(code,reader,pool,1);break; }
			case 0x45:  {byteCode = new ByteCode_fstore(code,reader,pool,2);break; }
			case 0x46:  {byteCode = new ByteCode_fstore(code,reader,pool,3);break; }
			case 0x47:  {byteCode = new ByteCode_dstore(code,reader,pool,0);break; }
			case 0x48:  {byteCode = new ByteCode_dstore(code,reader,pool,1);break; }
			case 0x49:  {byteCode = new ByteCode_dstore(code,reader,pool,2);break; }
			case 0x4A:  {byteCode = new ByteCode_dstore(code,reader,pool,3);break; }
			case 0x4B:  {byteCode = new ByteCode_astore(code,reader,pool,0); break; }
			case 0x4C:  {byteCode = new ByteCode_astore(code,reader,pool,1); break; }
			case 0x4D:  {byteCode = new ByteCode_astore(code,reader,pool,2); break; }
			case 0x4E:  {byteCode = new ByteCode_astore(code,reader,pool,3); break; }
			case 0x4F:  {byteCode = new ByteCode_iastore(code,reader,pool); break; }
			case 0x53: 	{byteCode = new ByteCode_aastore(code,reader,pool);break; }
			case 0x54: 	{byteCode = new ByteCode_bastore(code,reader,pool);break; }
			case 0x55: 	{byteCode = new ByteCode_castore(code,reader,pool);break; }
			case 0x57: 	{byteCode = new ByteCode_pop(code,reader,pool,1);break; }
			case 0x58: 	{byteCode = new ByteCode_pop(code,reader,pool,2);break; }
			case 0x59: 	{byteCode = new ByteCode_dup(code,reader,pool,0);break; }
			case 0x5A: 	{byteCode = new ByteCode_dup(code,reader,pool,1);break; }
			case 0x5B: 	{byteCode = new ByteCode_dup(code,reader,pool,2);break; }
			case 0x5C: 	{byteCode = new ByteCode_dup2(code,reader,pool,0);break; }	
			case 0x60: 	{byteCode = new ByteCode_iadd(code,reader,pool);break; }
			case 0x61: 	{byteCode = new ByteCode_ladd(code,reader,pool);break; }
			case 0x62: 	{byteCode = new ByteCode_fadd(code,reader,pool);break; }
			case 0x63: 	{byteCode = new ByteCode_dadd(code,reader,pool);break; }
			case 0x64: 	{byteCode = new ByteCode_isub(code,reader,pool);break; }
			case 0x65: 	{byteCode = new ByteCode_lsub(code,reader,pool);break; }
			case 0x66: 	{byteCode = new ByteCode_fsub(code,reader,pool);break; }
			case 0x67: 	{byteCode = new ByteCode_dsub(code,reader,pool);break; }
			case 0x68: 	{byteCode = new ByteCode_imul(code,reader,pool);break; }
			case 0x6A: 	{byteCode = new ByteCode_fmul(code,reader,pool);break; }
			case 0x6B: 	{byteCode = new ByteCode_dmul(code,reader,pool);break; }
			case 0x6C: 	{byteCode = new ByteCode_idiv(code,reader,pool);break; }
			case 0x6D: 	{byteCode = new ByteCode_ldiv(code,reader,pool);break; }
			case 0x6F: 	{byteCode = new ByteCode_ddiv(code,reader,pool);break; }
			case 0x70: 	{byteCode = new ByteCode_irem(code,reader,pool);break; }
			case 0x74: 	{byteCode = new ByteCode_ineg(code,reader,pool);break; }
			case 0x75: 	{byteCode = new ByteCode_lneg(code,reader,pool);break; }
			case 0x76: 	{byteCode = new ByteCode_fneg(code,reader,pool);break; }
			case 0x77: 	{byteCode = new ByteCode_dneg(code,reader,pool);break; }
			case 0x78: 	{byteCode = new ByteCode_ishl(code,reader,pool);break; }
			case 0x79: 	{byteCode = new ByteCode_lshl(code,reader,pool);break; }
			case 0x7A: 	{byteCode = new ByteCode_ishr(code,reader,pool);break; }
			case 0x7C: 	{byteCode = new ByteCode_iushr(code,reader,pool);break; }
			case 0x7D: 	{byteCode = new ByteCode_lushr(code,reader,pool);break; }
			case 0x7E: 	{byteCode = new ByteCode_iand(code,reader,pool);break; }
			case 0x7F: 	{byteCode = new ByteCode_land(code,reader,pool);break; }
			case 0x80: 	{byteCode = new ByteCode_ior(code,reader,pool);break; }
			case 0x81: 	{byteCode = new ByteCode_lor(code,reader,pool);break; }
			case 0x82: 	{byteCode = new ByteCode_ixor(code,reader,pool);break; }	
			case 0x84: 	{byteCode = new ByteCode_iinc(code,reader,pool);break; }
			case 0x85: 	{byteCode = new ByteCode_i2l(code,reader,pool);break; }
			case 0x86: 	{byteCode = new ByteCode_i2f(code,reader,pool);break; }
			case 0x87: 	{byteCode = new ByteCode_i2d(code,reader,pool);break; }
			case 0x88: 	{byteCode = new ByteCode_l2i(code,reader,pool);break; }
			case 0x8B: 	{byteCode = new ByteCode_f2i(code,reader,pool);break; }
			case 0x8D: 	{byteCode = new ByteCode_f2d(code,reader,pool);break; }
			case 0x8E: 	{byteCode = new ByteCode_d2i(code,reader,pool);break; }
			case 0x8F: 	{byteCode = new ByteCode_d2l(code,reader,pool);break; }
			case 0x91: 	{byteCode = new ByteCode_i2b(code,reader,pool);break; }
			case 0x92: 	{byteCode = new ByteCode_i2c(code,reader,pool);break; }
			case 0x93: 	{byteCode = new ByteCode_i2s(code,reader,pool);break; }
			case 0x94: 	{byteCode = new ByteCode_lcmp(code,reader,pool);break; }
			case 0x95: 	{byteCode = new ByteCode_fcmp(code,reader,pool,"l");break; }
			case 0x96: 	{byteCode = new ByteCode_fcmp(code,reader,pool,"g");break; }
			case 0x97: 	{byteCode = new ByteCode_dcmp(code,reader,pool,"l");break; }
			case 0x98: 	{byteCode = new ByteCode_dcmp(code,reader,pool,"g");break; }			
			case 0x99: 	{byteCode = new ByteCode_if(code,reader,pool,"eq");break; }
			case 0x9A: 	{byteCode = new ByteCode_if(code,reader,pool,"ne");break; }
			case 0x9B: 	{byteCode = new ByteCode_if(code,reader,pool,"lt");break; }
			case 0x9C: 	{byteCode = new ByteCode_if(code,reader,pool,"ge");break; }
			case 0x9D: 	{byteCode = new ByteCode_if(code,reader,pool,"gt");break; }
			case 0x9E: 	{byteCode = new ByteCode_if(code,reader,pool,"le");break; }				
			case 0x9F: 	{byteCode = new ByteCode_if_icmp(code,reader,pool,"eq");break; }				
			case 0xA0:  {byteCode = new ByteCode_if_icmp(code,reader,pool,"ne");break; }
			case 0xA1:  {byteCode = new ByteCode_if_icmp(code,reader,pool,"lt");break; }
			case 0xA2:  {byteCode = new ByteCode_if_icmp(code,reader,pool,"ge");break; }
			case 0xA3:  {byteCode = new ByteCode_if_icmp(code,reader,pool,"gt");break; }
			case 0xA4:  {byteCode = new ByteCode_if_icmp(code,reader,pool,"le");break; }
			case 0xA5:  {byteCode = new ByteCode_if_acmp(code,reader,pool,"eq");break; }	
			case 0xA6:  {byteCode = new ByteCode_if_acmp(code,reader,pool,"ne");break; }
			case 0xA7:  {byteCode = new ByteCode_goto(code,reader,pool);break; }
			case 0xA8:  {byteCode = new ByteCode_jsr(code,reader,pool);break; }
			case 0xA9:  {byteCode = new ByteCode_ret(code,reader,pool);break; }
			case 0xAA:  {byteCode = new ByteCode_tableswitch(code,reader,pool,offset);break; }
			case 0xAB:  {byteCode = new ByteCode_lookupswitch(code,reader,pool,offset);break; }
			case 0xAC:  {byteCode = new ByteCode_ireturn(code,reader,pool); break; }
			case 0xAD:  {byteCode = new ByteCode_lreturn(code,reader,pool); break; }
			case 0xAE:  {byteCode = new ByteCode_freturn(code,reader,pool); break; }
			case 0xAF:  {byteCode = new ByteCode_dreturn(code,reader,pool); break; }
			case 0xB0:  {byteCode = new ByteCode_areturn(code,reader,pool); break; }
			case 0xB1:  {byteCode = new ByteCode_return(code,reader,pool); break; }
			case 0xB2:  {byteCode = new ByteCode_getstatic(code,reader,pool); break; }
			case 0xB3:  {byteCode = new ByteCode_putstatic(code,reader,pool); break; }
			case 0xB4:  {byteCode = new ByteCode_getfield(code,reader,pool); break; }
			case 0xB5:  {byteCode = new ByteCode_putfield(code,reader,pool); break; }
			case 0xB6:  {byteCode = new ByteCode_invokevirtual(code,reader,pool); break; }
			case 0xB7:  {byteCode = new ByteCode_invokespecial(code,reader,pool); break; }
			case 0xB8:  {byteCode = new ByteCode_invokestatic(code,reader,pool); break; }
			case 0xB9:  {byteCode = new ByteCode_invokeinterface(code,reader,pool); break; }
			case 0xBB:  {byteCode = new ByteCode_new(code,reader,pool); break; }
			case 0xBC:  {byteCode = new ByteCode_newarray(code,reader,pool); break; }
			case 0xBD:  {byteCode = new ByteCode_anewarray(code,reader,pool); break; }
			case 0xBE:  {byteCode = new ByteCode_arraylength(code,reader,pool); break; }	
			case 0xBF:  {byteCode = new ByteCode_athrow(code,reader,pool); break; }
			case 0xC0:  {byteCode = new ByteCode_checkcast(code,reader,pool); break; }
			case 0xC1:  {byteCode = new ByteCode_instanceof(code,reader,pool); break; }
			case 0xC2:  {byteCode = new ByteCode_monitorenter(code,reader,pool); break; }
			case 0xC3:  {byteCode = new ByteCode_monitorexit(code,reader,pool); break; }
				case 0xC6:  {byteCode = new ByteCode_if(code,reader,pool,"null"); break; }
				case 0xC7:  {byteCode = new ByteCode_if(code,reader,pool,"nonnull"); break; }
				default: 
				{
					throw new UnsupportedByteCodeException(String.Format("Unknown code {0}/{0:X}",code));
				}
			}

			
			return byteCode;
		}


	}

	public class UnsupportedByteCodeException : Exception 
	{
		public UnsupportedByteCodeException(String message) : base(message)
		{
		}
	}
}
